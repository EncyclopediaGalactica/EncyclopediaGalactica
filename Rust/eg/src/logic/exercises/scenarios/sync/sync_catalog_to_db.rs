use std::fs;
use std::path::Path;
use std::path::PathBuf;
use std::str::FromStr;

use anyhow::Context;
use anyhow::Ok;
use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use crate::logic::exercises::repository::chapter::find_chapter_id_by_chapter_reference_and_book_id::find_chapter_id_by_chapter_reference_and_book_id;
use crate::logic::exercises::repository::sections::find_section_id_by_section_reference_and_chapter_id::find_section_id_by_section_reference_and_chapter_id;
use crate::ExercisesConfig;
use crate::logic::exercises::catalog_scanner::scan_and_collect_catalog_files_by_pattern;
use crate::logic::exercises::catalog_scanner::scan_and_collect_catalog_files_by_wildcard_pattern;
use crate::logic::exercises::parsers::added_exercises::parse_added_exercise_files;
use crate::logic::exercises::parsers::books::parse_book_files;
use crate::logic::exercises::parsers::chapters::parse_chapter_files;
use crate::logic::exercises::parsers::sections::parse_section_files;
use crate::logic::exercises::parsers::topics::parse_topic_files;
use crate::logic::exercises::repository::book::BookEntity;
use crate::logic::exercises::repository::book::add::add_book;
use crate::logic::exercises::repository::book::find_book_id_by_reference::find_book_id_by_reference;
use crate::logic::exercises::repository::book::truncate::truncate_books_table;
use crate::logic::exercises::repository::chapter::ChapterEntity;
use crate::logic::exercises::repository::chapter::add::add_chapter;
use crate::logic::exercises::repository::chapter::truncate::truncate_chapters_table;
use crate::logic::exercises::repository::exercises::ExerciseEntity;
use crate::logic::exercises::repository::exercises::ExerciseType;
use crate::logic::exercises::repository::exercises::RawExerciseEntity;
use crate::logic::exercises::repository::exercises::add_exercise::add_exercise;
use crate::logic::exercises::repository::exercises::find_all_raw_exercise_entities::find_all_raw_exercise_entities;
use crate::logic::exercises::repository::exercises::truncate::truncate_exercises_table;
use crate::logic::exercises::repository::get_connection;
use crate::logic::exercises::repository::sections::SectionEntity;
use crate::logic::exercises::repository::sections::add::add_section;
use crate::logic::exercises::repository::sections::truncate::truncate_sections_table;
use crate::logic::exercises::repository::topic::add::add_topic;
use crate::logic::exercises::repository::topic::get_topic_id_by_reference::find_topic_id_by_reference;
use crate::logic::exercises::repository::topic::truncate::truncate_topics_table;

pub async fn sync_catalog_to_db(config: ExercisesConfig) -> anyhow::Result<()> {
    let book_catalog_absolute_path = canonicalize_path_from_config(config.clone().catalog_path)?;
    let db_connection = get_connection(&config.database_connection_string).await?;
    truncate_topics_table(db_connection.clone())
        .await
        .context("Failed to truncate topics table")?;
    debug!("Truncated topics table");

    truncate_books_table(db_connection.clone())
        .await
        .context("Failed to truncate books table")?;
    debug!("Truncated books table");

    truncate_chapters_table(db_connection.clone())
        .await
        .context("Failed to truncate chapters table")?;
    debug!("Truncated chapters table");

    truncate_sections_table(db_connection.clone())
        .await
        .context("Failed to truncate sections table")?;
    debug!("Truncated sections table");

    truncate_exercises_table(db_connection.clone())
        .await
        .context("Failed to truncate exercises table")?;
    debug!("Truncated exercises table");

    sync_topics_to_db(book_catalog_absolute_path.clone(), db_connection.clone())
        .await
        .with_context(|| {
            format!(
                "Failed to sync topics to db. book_catalog_absolute_path: {:#?}",
                book_catalog_absolute_path
            )
        })?;
    debug!("Synced topics to db");

    sync_books_to_db(book_catalog_absolute_path.clone(), db_connection.clone())
        .await
        .with_context(|| {
            format!(
                "Failed to sync books to db. book_catalog_absolute_path: {:#?}",
                book_catalog_absolute_path
            )
        })?;
    debug!("Synced books to db");

    sync_chapters_to_db(book_catalog_absolute_path.clone(), db_connection.clone())
        .await
        .with_context(|| {
            format!(
                "Failed to sync chapters to db. book_catalog_absolute_path: {:#?}",
                book_catalog_absolute_path
            )
        })?;
    debug!("Synced chapters to db");

    sync_sections_to_db(book_catalog_absolute_path.clone(), db_connection.clone())
        .await
        .with_context(|| {
            format!(
                "Failed to sync sections to db. book_catalog_absolute_path: {:#?}",
                book_catalog_absolute_path
            )
        })?;
    debug!("Synced sections to db");

    sync_textbook_exercises_to_db(db_connection.clone())
        .await
        .with_context(|| format!("Failed to sync textbook exercises to db"))?;
    debug!("Synced textbook exercises to db");

    sync_added_exercises_to_db(book_catalog_absolute_path.clone(), db_connection.clone())
        .await
        .with_context(|| {
            format!(
                "Failed to sync added exercises to db. book_catalog_absolute_path: {:#?}",
                book_catalog_absolute_path
            )
        })?;
    debug!("Synced added exercises to db");

    Ok(())
}

async fn sync_added_exercises_to_db(
    book_catalog_absolute_path: PathBuf,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    let added_exercise_files = scan_and_collect_catalog_files_by_wildcard_pattern(
        book_catalog_absolute_path.clone(),
        "exercise_*.tex",
    )?;
    let parsed_added_exercises = parse_added_exercise_files(added_exercise_files)?;
    let mut exercises: Vec<ExerciseEntity> = Vec::new();
    for added_exercise in parsed_added_exercises {
        let mut exercise = ExerciseEntity::default();
        if added_exercise.topic_reference() != "" {
            let topic_id =
                find_topic_id_by_reference(added_exercise.topic_reference(), db_connection.clone())
                    .await?;
            exercise.topic_id = topic_id;
        } else {
            anyhow::bail!("At least topic reference is required.");
        }
        if added_exercise.book_reference() != ""
            && added_exercise.book_reference() != "NA"
            && exercise.topic_id != 0
        {
            let book_id =
                find_book_id_by_reference(added_exercise.book_reference(), db_connection.clone())
                    .await?;
            exercise.book_id = book_id;
        } else {
            anyhow::bail!(
                r#"
            The 
            `added_exercise.book_reference() != ""
            && added_exercise.book_reference() != "NA"
            && exercise.topic_id != 0`
            conditions are not met.
            "#
            );
        }
        if added_exercise.chapter_reference() != ""
            && added_exercise.chapter_reference() != "NA"
            && exercise.book_id != 0
            && exercise.topic_id != 0
        {
            let chapter_id = find_chapter_id_by_chapter_reference_and_book_id(
                added_exercise.chapter_reference(),
                exercise.book_id,
                db_connection.clone(),
            )
            .await?;
            exercise.chapter_id = chapter_id;
        } else {
            anyhow::bail!(
                r#"
            The
            `added_exercise.chapter_reference() != ""
            && added_exercise.chapter_reference() != "NA"
            && exercise.book_id != 0
            && exercise.topic_id != 0`
            conditions are not met.
            "#
            );
        }
        if added_exercise.section_reference() != ""
            && added_exercise.section_reference() != "NA"
            && exercise.book_id != 0
            && exercise.topic_id != 0
            && exercise.chapter_id != 0
        {
            let section_id = find_section_id_by_section_reference_and_chapter_id(
                added_exercise.section_reference(),
                exercise.chapter_id,
                db_connection.clone(),
            )
            .await?;
            exercise.section_id = section_id;
        } else {
            anyhow::bail!(
                r#"
            The
            `added_exercise.section_reference() != ""
            && added_exercise.section_reference() != "NA"
            && exercise.book_id != 0
            && exercise.topic_id != 0
            && exercise.chapter_id != 0`
            are not met.
            "#
            );
        }
        if added_exercise.exercise_type() != "" {
            exercise.exercise_type = ExerciseType::from_str(added_exercise.exercise_type())
                .unwrap()
                .to_string();
        }
        exercise.manual_id = added_exercise.manual_id();
        exercise.question = added_exercise.question().to_string();
        exercise.solution = added_exercise.solution().to_string();
        exercises.push(exercise);
    }
    for exercise in exercises {
        add_exercise(exercise.clone(), db_connection.clone())
            .await
            .with_context(|| format!("Failed to add exercise to database: {:#?}", exercise))?;
    }
    Ok(())
}

async fn sync_textbook_exercises_to_db(db_connection: Pool<Postgres>) -> anyhow::Result<()> {
    let raw_exercises = find_all_raw_exercise_entities(db_connection.clone()).await?;
    let mut exercises: Vec<ExerciseEntity> = Vec::new();
    for raw_exercise in raw_exercises {
        let mut skill_exercises = create_skill_exercises(raw_exercise.clone())?;
        exercises.append(&mut skill_exercises);
        let mut concept_exercises = create_concept_exercises(raw_exercise.clone())?;
        exercises.append(&mut concept_exercises);
        let mut applications_exercises = create_applications_exercises(raw_exercise.clone())?;
        exercises.append(&mut applications_exercises);
        let mut discussion_exercises = create_discussion_exercises(raw_exercise.clone())?;
        exercises.append(&mut discussion_exercises);
    }
    for exercise in exercises {
        add_exercise(exercise, db_connection.clone()).await?;
    }
    Ok(())
}

fn create_discussion_exercises(
    raw_exercise: RawExerciseEntity,
) -> anyhow::Result<Vec<ExerciseEntity>> {
    let mut discussion_exercises: Vec<ExerciseEntity> = Vec::new();
    for exercise_id_in_book in raw_exercise.discussion_questions_interval_start
        ..raw_exercise.discussion_questions_interval_end
    {
        discussion_exercises.push(ExerciseEntity {
            id: 0,
            id_in_book: exercise_id_in_book,
            manual_id: 0,
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Discussion.to_string(),
            question: String::from(""),
            solution: String::from(""),
        });
    }
    Ok(discussion_exercises)
}

fn create_applications_exercises(
    raw_exercise: RawExerciseEntity,
) -> anyhow::Result<Vec<ExerciseEntity>> {
    let mut application_exercises: Vec<ExerciseEntity> = Vec::new();
    for exercise_id_in_book in raw_exercise.applications_questions_interval_start
        ..raw_exercise.applications_questions_interval_end
    {
        application_exercises.push(ExerciseEntity {
            id: 0,
            id_in_book: exercise_id_in_book,
            manual_id: 0,
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Applications.to_string(),
            question: String::from(""),
            solution: String::from(""),
        });
    }
    Ok(application_exercises)
}

fn create_concept_exercises(
    raw_exercise: RawExerciseEntity,
) -> anyhow::Result<Vec<ExerciseEntity>> {
    let mut concept_exercises: Vec<ExerciseEntity> = Vec::new();
    for exercise_id_in_book in
        raw_exercise.concepts_questions_interval_start..raw_exercise.concepts_questions_interval_end
    {
        concept_exercises.push(ExerciseEntity {
            id: 0,
            id_in_book: exercise_id_in_book,
            manual_id: 0,
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Concepts.to_string(),
            question: String::from(""),
            solution: String::from(""),
        });
    }
    Ok(concept_exercises)
}

fn create_skill_exercises(raw_exercise: RawExerciseEntity) -> anyhow::Result<Vec<ExerciseEntity>> {
    let mut skill_exercises: Vec<ExerciseEntity> = Vec::new();
    for exercise_id_in_book in
        raw_exercise.skills_questions_interval_start..raw_exercise.skills_questions_interval_end
    {
        skill_exercises.push(ExerciseEntity {
            id: 0,
            id_in_book: exercise_id_in_book,
            manual_id: 0,
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Skills.to_string(),
            question: String::from(""),
            solution: String::from(""),
        });
    }
    Ok(skill_exercises)
}

async fn sync_sections_to_db(
    book_catalog_absolute_path: PathBuf,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    let section_files_with_absolute_path = scan_and_collect_catalog_files_by_pattern(
        book_catalog_absolute_path.clone(),
        "section.toml",
    )?;
    let parsed_sections = parse_section_files(section_files_with_absolute_path)?;
    if parsed_sections.is_empty() {
        anyhow::bail!("Parsed sections list is empty");
    }

    for section in parsed_sections {
        add_section(
            SectionEntity::from(section.clone()),
            section.chapter_reference(),
            section.book_reference(),
            db_connection.clone(),
        )
        .await?;
    }
    Ok(())
}

async fn sync_chapters_to_db(
    book_catalog_absolute_path: PathBuf,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    let chapter_files_with_absolute_path = scan_and_collect_catalog_files_by_pattern(
        book_catalog_absolute_path.clone(),
        "chapter.toml",
    )?;
    let parsed_chapters = parse_chapter_files(chapter_files_with_absolute_path)?;
    if parsed_chapters.is_empty() {
        anyhow::bail!("Parsed chapter list is empty");
    }

    for chapter in parsed_chapters {
        add_chapter(
            ChapterEntity::from(chapter.clone()),
            chapter.book_reference(),
            db_connection.clone(),
        )
        .await?;
    }
    Ok(())
}

async fn sync_books_to_db(
    book_catalog_absolute_path: PathBuf,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    let book_files_with_absolute_path =
        scan_and_collect_catalog_files_by_pattern(book_catalog_absolute_path.clone(), "book.toml")?;
    let parsed_books = parse_book_files(book_files_with_absolute_path)?;
    if parsed_books.is_empty() {
        anyhow::bail!("Parsed books list is empty");
    }
    // truncate_books_table(db_connection.clone()).await?;

    for book in parsed_books {
        add_book(
            BookEntity::from(book.clone()),
            book.topic_reference(),
            db_connection.clone(),
        )
        .await?;
    }
    Ok(())
}

async fn sync_topics_to_db(
    book_catalog_absolute_path: PathBuf,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    let topic_files_with_absolute_path = scan_and_collect_catalog_files_by_pattern(
        book_catalog_absolute_path.clone(),
        "topic.toml",
    )?;
    let parsed_topics = parse_topic_files(topic_files_with_absolute_path)?;
    if parsed_topics.is_empty() {
        anyhow::bail!("Parsed topics list is empty");
    }

    for topic in parsed_topics {
        add_topic(topic.into(), db_connection.clone()).await?;
    }
    Ok(())
}

fn canonicalize_path_from_config(path: String) -> anyhow::Result<PathBuf> {
    let path = Path::new(&path);
    Ok(fs::canonicalize(path)?)
}
