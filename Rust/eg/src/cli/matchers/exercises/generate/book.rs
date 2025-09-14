use clap::ArgMatches;

use crate::ExercisesConfig;
use crate::logic::exercises::scenarios::generate::book::ExercisesGenerateBookScenarioInput;
use crate::logic::exercises::scenarios::generate::book::exercises_generate_book_scenario;

pub async fn exercises_generate_book_matchers(
    args: ArgMatches,
    config: ExercisesConfig,
) -> anyhow::Result<()> {
    let book_reference = args.get_one::<String>("BOOKS").unwrap();

    let chapters_input = args.get_one::<String>("CHAPTERS").unwrap();
    let mut chapters = vec![];
    if !chapters_input.is_empty() && chapters_input.contains(&String::from(",")) {
        chapters_input
            .rsplit(',')
            .map(|s| chapters.push(s.to_string()));
    } else if !chapters_input.is_empty() && !chapters_input.contains(&String::from(",")) {
        chapters.push(chapters_input.to_string());
    }

    let concept_questions_volume = args
        .get_one::<usize>("CONCEPT QUESTIONS VOLUME")
        .unwrap_or_else(|| &0);
    let skill_questions_volume = args
        .get_one::<usize>("SKILL QUESTIONS VOLUME")
        .unwrap_or_else(|| &0);
    let application_questions_volume = args
        .get_one::<usize>("APPLICATION QUESTIONS VOLUME")
        .unwrap_or_else(|| &0);
    let discussion_questions_volume = args
        .get_one::<usize>("DISCUSSION QUESTIONS VOLUME")
        .unwrap_or_else(|| &0);

    let exercises_generate_book_scenario_input = ExercisesGenerateBookScenarioInput {
        target_directory: config.generated_tests_path,
        book_reference: book_reference.to_string(),
        chapters: chapters,
        concept_questions_volume: *concept_questions_volume,
        skill_questions_volume: *skill_questions_volume,
        application_questions_volume: *application_questions_volume,
        discussion_questions_volume: *discussion_questions_volume,
    };

    match exercises_generate_book_scenario(
        exercises_generate_book_scenario_input.clone(),
        config.clone(),
    )
    .await
    {
        Ok(_) => Ok(()),
        Err(e) => Err(e),
    }
}
