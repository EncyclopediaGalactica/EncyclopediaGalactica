use std::fs::canonicalize;

use log::debug;
use rand::Rng;

use crate::logic::exercises::providers::provide_filename;
use crate::logic::exercises::renderers::latex::render_latex;
use crate::logic::exercises::repository::chapter::find_chapter_ids_by_book_reference::find_chapter_ids_by_book_reference;
use crate::logic::exercises::repository::chapter::find_chapter_ids_by_references_and_book_reference::find_chapter_ids_by_references_and_book_reference;
use crate::logic::exercises::repository::exercises::find_exercises_by_chapter_ids_and_type::find_exercises_by_chapter_ids_and_type;
use crate::logic::exercises::repository::exercises::find_exercises_by_ids::find_exercises_by_ids;
use crate::logic::exercises::repository::exercises::find_exercises_by_ids::EnrichedExerciseEntity;
use crate::logic::exercises::repository::exercises::ExerciseType;
use crate::logic::exercises::repository::book::find_book_id_by_reference::find_book_id_by_reference;
use crate::logic::exercises::repository::get_connection;

pub async fn exercises_generate_book_scenario(
    input: ExercisesGenerateBookScenarioInput,
) -> anyhow::Result<()> {
    debug!(
        "`eg exercises generate book` scenario has been started. Config: {:#?}",
        input
    );
    let db_connection = get_connection(&input.db_connection_string).await?;
    debug!("Connected to database.");

    let mut rand = rand::rng();
    validate_scenario_input(input.clone())?;
    debug!("Input has been validated.");

    match find_book_id_by_reference(&input.book_reference, db_connection.clone()).await {
        Ok(yolo) => {
            if yolo == 0 {
                debug!("Book with reference {} not found.", input.book_reference);
                return Err(anyhow::anyhow!(
                    "Book with reference {} not found.",
                    input.book_reference
                ));
            } else {
                debug!("Book with reference {} found.", input.book_reference);
            }
        }
        Err(nopes) => {
            debug!("Failed to execute database query: {:#?}", nopes);
            return Err(anyhow::anyhow!("Failed to find book: {:#?}", nopes));
        }
    };

    let mut chapter_ids: Vec<i64> = Vec::new();
    if input.chapters.len() == 1 && input.chapters.get(0).unwrap() == "all" {
        let mut result =
            find_chapter_ids_by_book_reference(input.book_reference.clone(), db_connection.clone())
                .await?;
        debug!(
            "Found chapter ids based on that NO chapter ids were provided: {:#?}",
            result.len()
        );
        chapter_ids.append(&mut result);
    } else {
        let mut result = find_chapter_ids_by_references_and_book_reference(
            input.chapters.clone(),
            input.book_reference.clone(),
            db_connection.clone(),
        )
        .await?;
        debug!(
            "Found chapter ids based on the provided chapter references: {:#?}",
            result.len()
        );
        chapter_ids.append(&mut result);
    }
    debug!("Chapter ids volume: {:#?}", chapter_ids.len());

    let mut exercise_candidates: Vec<i64> = Vec::new();
    if input.concept_questions_volume > 0 {
        debug!("Populating concept exercise candidates.");
        let concept_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Concepts,
            db_connection.clone(),
        )
        .await?;
        if !concept_exercise_candidates.is_empty() {
            let candidates_len = concept_exercise_candidates.len();
            debug!("Found {} concept exercise candidates.", candidates_len);
            debug!(
                "Selecting {} volume concept exercises.",
                input.concept_questions_volume
            );
            for counter in 0..input.concept_questions_volume {
                let randomly_selected_concept_exercise = rand.random_range(0..candidates_len + 1);
                debug!(
                    "Adding concept exercise with id {}",
                    randomly_selected_concept_exercise
                );
                exercise_candidates.push(
                    concept_exercise_candidates
                        .get(randomly_selected_concept_exercise)
                        .unwrap()
                        .to_owned(),
                );
            }
        }
    }

    if input.skill_questions_volume > 0 {
        debug!("Populating Skills type exercises candidates.");
        let skill_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Skills,
            db_connection.clone(),
        )
        .await?;
        debug!(
            "Found {} Skills type exercises.",
            skill_exercise_candidates.len()
        );
        debug!(
            "Selecting {} volume Skills type exercises.",
            input.skill_questions_volume
        );
        if !skill_exercise_candidates.is_empty() {
            for counter in 0..input.skill_questions_volume {
                let randomly_selected_skill_exercise =
                    rand.random_range(0..skill_exercise_candidates.len() + 1);
                debug!(
                    "Adding Skills type exercise with id {}",
                    randomly_selected_skill_exercise
                );
                exercise_candidates.push(
                    skill_exercise_candidates
                        .get(randomly_selected_skill_exercise)
                        .unwrap()
                        .to_owned(),
                );
            }
        }
    }

    if input.application_questions_volume > 0 {
        debug!("Populating Applications type exercises candidates.");
        let application_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Applications,
            db_connection.clone(),
        )
        .await?;
        debug!(
            "Found {} Applications type exercises.",
            application_exercise_candidates.len()
        );
        debug!(
            "Selecting {} volume Applications type exercises.",
            input.application_questions_volume
        );
        if !application_exercise_candidates.is_empty() {
            for counter in 0..input.application_questions_volume {
                let randomly_selected_application_exercise =
                    rand.random_range(0..application_exercise_candidates.len() + 1);
                debug!(
                    "Adding Applications type exercise with id {}",
                    randomly_selected_application_exercise
                );
                exercise_candidates.push(
                    application_exercise_candidates
                        .get(randomly_selected_application_exercise)
                        .unwrap()
                        .to_owned(),
                );
            }
        }
    }

    if input.discussion_questions_volume > 0 {
        debug!("Populating Discussions type exercises candidates.");
        let discussion_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Applications,
            db_connection.clone(),
        )
        .await?;
        debug!(
            "Found {} Discussions type exercises.",
            discussion_exercise_candidates.len()
        );
        debug!(
            "Selecting {} volume Discussions type exercises.",
            input.discussion_questions_volume
        );
        if !discussion_exercise_candidates.is_empty() {
            for counter in 0..input.discussion_questions_volume {
                let randomly_selected_discussion_exercise =
                    rand.random_range(0..discussion_exercise_candidates.len() + 1);
                debug!(
                    "Adding Discussions type exercise with id {}",
                    randomly_selected_discussion_exercise
                );
                exercise_candidates.push(
                    discussion_exercise_candidates
                        .get(randomly_selected_discussion_exercise)
                        .unwrap()
                        .to_owned(),
                );
            }
        }
    }

    let final_exercises: Vec<EnrichedExerciseEntity> =
        find_exercises_by_ids(exercise_candidates, db_connection.clone()).await?;
    let filename = provide_filename()?;
    debug!("filename: {:#?}", filename);
    let absolute_path_for_filename = canonicalize(input.generated_tests_path.clone())?;
    debug!(
        "target directory absolute path: {:#?}",
        absolute_path_for_filename
    );
    let target_absolute_path_with_filename = absolute_path_for_filename.join(filename.clone());
    debug!(
        "Absolute path with filename: {:#?}",
        target_absolute_path_with_filename
    );
    render_latex(final_exercises, filename.clone())?;

    Ok(())
}

fn validate_scenario_input(input: ExercisesGenerateBookScenarioInput) -> anyhow::Result<()> {
    if input.book_reference.is_empty() {
        return Err(anyhow::anyhow!("book_reference must be provided."));
    }
    if input.generated_tests_path.is_empty() {
        return Err(anyhow::anyhow!("target_directory must be provided."));
    }
    Ok(())
}

#[derive(Debug, Clone)]
pub struct ExercisesGenerateBookScenarioResult {}

#[derive(Debug, Clone)]
pub struct ExercisesGenerateBookScenarioInput {
    pub generated_tests_path: String,
    pub db_connection_string: String,
    pub book_reference: String,
    pub chapters: Vec<String>,
    pub concept_questions_volume: usize,
    pub skill_questions_volume: usize,
    pub application_questions_volume: usize,
    pub discussion_questions_volume: usize,
}
