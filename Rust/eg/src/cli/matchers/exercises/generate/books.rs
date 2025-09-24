use clap::ArgMatches;
use env_logger::Builder;

use crate::ExercisesConfig;
use crate::cli::matchers::set_cli_logging_level;
use crate::logic::exercises::scenarios::generate::books::ExercisesGenerateBooksScenarioInput;
use crate::logic::exercises::scenarios::generate::books::exercises_generate_books_scenario;

pub async fn exercises_generate_books_matchers(
    args: ArgMatches,
    config: ExercisesConfig,
) -> anyhow::Result<()> {
    let book_references = args
        .get_one::<String>("BOOKS")
        .ok_or_else(|| anyhow::anyhow!("BOOKS is required"))
        .unwrap();
    let mut books = vec![];
    book_references
        .rsplit(',')
        .map(|f| books.push(f.trim().to_string()));

    let chapters_input = args
        .get_one::<String>("CHAPTERS")
        .map(|s| s.as_str())
        .unwrap_or("all");
    let mut chapters = vec![];
    if !chapters_input.is_empty() && chapters_input.contains(&String::from(",")) {
        let _ = chapters_input
            .rsplit(',')
            .map(|s| chapters.push(s.trim().to_string()));
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

    Builder::new()
        .filter(
            None,
            set_cli_logging_level(args.clone()).unwrap_or_else(|_| log::LevelFilter::Off),
        )
        .init();

    let exercises_generate_books_scenario_input = ExercisesGenerateBooksScenarioInput {
        book_catalog_path: config.catalog_path,
        generated_tests_path: config.generated_tests_path,
        db_connection_string: config.database_connection_string.clone(),
        book_references: books,
        chapters: chapters,
        concept_questions_volume: *concept_questions_volume,
        skill_questions_volume: *skill_questions_volume,
        application_questions_volume: *application_questions_volume,
        discussion_questions_volume: *discussion_questions_volume,
    };

    match exercises_generate_books_scenario(exercises_generate_books_scenario_input.clone()).await {
        Ok(_) => Ok(()),
        Err(e) => Err(e),
    }
}
