use std::fs;
use std::path::Path;
use std::path::PathBuf;

use anyhow::Ok;
use sqlx::Pool;
use sqlx::Postgres;

use crate::ExercisesConfig;
use crate::logic::exercises::catalog_scanner::scan_and_collect_catalog_files_by_pattern;
use crate::logic::exercises::parsers::books::parse_book_files;
use crate::logic::exercises::parsers::chapters::parse_chapter_files;
use crate::logic::exercises::parsers::sections::parse_section_files;
use crate::logic::exercises::parsers::topics::parse_topic_files;
use crate::logic::exercises::repository::book::BookEntity;
use crate::logic::exercises::repository::book::add::add_book;
use crate::logic::exercises::repository::book::truncate::truncate_books_table;
use crate::logic::exercises::repository::chapter::ChapterEntity;
use crate::logic::exercises::repository::chapter::add::add_chapter;
use crate::logic::exercises::repository::chapter::truncate::truncate_chapters_table;
use crate::logic::exercises::repository::exercises::ExerciseEntity;
use crate::logic::exercises::repository::exercises::ExerciseType;
use crate::logic::exercises::repository::exercises::RawExerciseEntity;
use crate::logic::exercises::repository::exercises::add_exercise::add_exercise;
use crate::logic::exercises::repository::exercises::find_all_raw_exercise_entities::find_all_raw_exercise_entities;
use crate::logic::exercises::repository::get_connection;
use crate::logic::exercises::repository::sections::SectionEntity;
use crate::logic::exercises::repository::sections::add::add_section;
use crate::logic::exercises::repository::sections::truncate::truncate_sections_table;
use crate::logic::exercises::repository::topic::add::add_topic;
use crate::logic::exercises::repository::topic::truncate::truncate_topics_table;

pub async fn sync_catalog_to_db(config: ExercisesConfig) -> anyhow::Result<()> {
    let book_catalog_absolute_path = canonicalize_path_from_config(config.clone().catalog_path)?;
    let db_connection = get_connection(&config.database_connection_string).await?;
    truncate_topics_table(db_connection.clone()).await?;
    truncate_books_table(db_connection.clone()).await?;
    truncate_chapters_table(db_connection.clone()).await?;
    truncate_sections_table(db_connection.clone()).await?;
    sync_topics_to_db(book_catalog_absolute_path.clone(), db_connection.clone()).await?;
    sync_books_to_db(book_catalog_absolute_path.clone(), db_connection.clone()).await?;
    sync_chapters_to_db(book_catalog_absolute_path.clone(), db_connection.clone()).await?;
    sync_sections_to_db(book_catalog_absolute_path.clone(), db_connection.clone()).await?;
    sync_exercises_to_db(db_connection.clone()).await?;
    Ok(())
}

async fn sync_exercises_to_db(db_connection: Pool<Postgres>) -> anyhow::Result<()> {
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
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Discussion.to_string(),
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
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Applications.to_string(),
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
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Concepts.to_string(),
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
            topic_id: raw_exercise.topic_id,
            book_id: raw_exercise.book_id,
            chapter_id: raw_exercise.chapter_id,
            section_id: raw_exercise.section_id,
            exercise_type: ExerciseType::Skills.to_string(),
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
    // truncate_topics_table(db_connection.clone()).await?;

    for topic in parsed_topics {
        add_topic(topic.into(), db_connection.clone()).await?;
    }
    Ok(())
}

fn canonicalize_path_from_config(path: String) -> anyhow::Result<PathBuf> {
    let path = Path::new(&path);
    Ok(fs::canonicalize(path)?)
}
