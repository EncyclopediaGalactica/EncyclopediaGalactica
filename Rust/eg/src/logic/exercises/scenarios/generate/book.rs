use crate::logic::exercises::repository::chapter::find_chapter_ids_by_references_and_book_reference::find_chapter_ids_by_references_and_book_reference;
use crate::logic::exercises::repository::exercises::find_exercises_by_chapter_ids_and_type::find_exercises_by_chapter_ids_and_type;
use crate::logic::exercises::repository::exercises::ExerciseType;
use crate::ExercisesConfig;
use crate::logic::exercises::repository::book::find_book_id_by_reference::find_book_id_by_reference;
use crate::logic::exercises::repository::get_connection;

pub async fn exercises_generate_book_scenario(
    input: ExercisesGenerateBookScenarioInput,
    config: ExercisesConfig,
) -> anyhow::Result<()> {
    let db_connection = get_connection(&config.database_connection_string).await?;
    validate_scenario_input(input.clone())?;
    match find_book_id_by_reference(&input.book_reference, db_connection.clone()).await {
        Ok(yolo) => {
            if yolo == 0 {
                return Err(anyhow::anyhow!(
                    "Book with reference {} not found.",
                    input.book_reference
                ));
            }
        }
        Err(nopes) => {
            return Err(anyhow::anyhow!("Failed to find book: {:#?}", nopes));
        }
    };

    let chapter_ids = find_chapter_ids_by_references_and_book_reference(
        input.chapters.clone(),
        input.book_reference.clone(),
        db_connection.clone(),
    )
    .await?;

    let mut exercise_candidates: Vec<i64> = Vec::new();
    if input.concept_questions_volume > 0 {
        let mut concept_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Concepts,
            db_connection.clone(),
        )
        .await?;
        if !concept_exercise_candidates.is_empty() {
            exercise_candidates.append(&mut concept_exercise_candidates);
        }
    }

    if input.skill_questions_volume > 0 {
        let mut skill_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Skills,
            db_connection.clone(),
        )
        .await?;
        if !skill_exercise_candidates.is_empty() {
            exercise_candidates.append(&mut skill_exercise_candidates);
        }
    }

    if input.application_questions_volume > 0 {
        let mut application_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Applications,
            db_connection.clone(),
        )
        .await?;
        if !application_exercise_candidates.is_empty() {
            exercise_candidates.append(&mut application_exercise_candidates);
        }
    }

    if input.discussion_questions_volume > 0 {
        let mut discussion_exercise_candidates = find_exercises_by_chapter_ids_and_type(
            chapter_ids.clone(),
            ExerciseType::Applications,
            db_connection.clone(),
        )
        .await?;
        if !discussion_exercise_candidates.is_empty() {
            exercise_candidates.append(&mut discussion_exercise_candidates);
        }
    }

    let final_exercises: Vec<ExercisesGenerateBookScenarioResult> = Vec::new();

    Ok(())
}

fn validate_scenario_input(input: ExercisesGenerateBookScenarioInput) -> anyhow::Result<()> {
    if input.book_reference.is_empty() {
        return Err(anyhow::anyhow!("book_reference must be provided."));
    }
    if input.target_directory.is_empty() {
        return Err(anyhow::anyhow!("target_directory must be provided."));
    }
    Ok(())
}

#[derive(Debug, Clone)]
pub struct ExercisesGenerateBookScenarioResult {}

#[derive(Debug, Clone)]
pub struct ExercisesGenerateBookScenarioInput {
    pub target_directory: String,
    pub book_reference: String,
    pub chapters: Vec<String>,
    pub concept_questions_volume: usize,
    pub skill_questions_volume: usize,
    pub application_questions_volume: usize,
    pub discussion_questions_volume: usize,
}
