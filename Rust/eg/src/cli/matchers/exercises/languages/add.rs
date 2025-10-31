use clap::ArgMatches;
use env_logger::Builder;

use crate::ExercisesConfig;
use crate::cli::matchers::set_cli_logging_level;
use crate::logic::exercises::scenarios::languages::dictionary::add::AddWordToDictionaryInput;
use crate::logic::exercises::scenarios::languages::dictionary::add::add_word_to_dictionary;

pub async fn exercises_languages_add_matchers(
    arguments: ArgMatches,
    config: ExercisesConfig,
) -> anyhow::Result<()> {
    Builder::new()
        .filter(
            None,
            set_cli_logging_level(arguments.clone()).unwrap_or_else(|_| log::LevelFilter::Off),
        )
        .init();

    let language_reference = arguments
        .get_one::<String>("LANGUAGE")
        .ok_or_else(|| anyhow::anyhow!("Language is required!"))
        .unwrap();

    let word_reference = arguments
        .get_one::<String>("WORD")
        .ok_or_else(|| anyhow::anyhow!("Word is required!"))
        .unwrap();

    let definition_reference = arguments
        .get_one::<String>("DEFINITION")
        .ok_or_else(|| anyhow::anyhow!("Definition is required!"))
        .unwrap();

    let scenario_input = AddWordToDictionaryInput::new(
        language_reference.to_string(),
        word_reference.to_string(),
        definition_reference.to_string(),
    );

    match add_word_to_dictionary(scenario_input.clone()).await? {
        Ok(_) => Ok(()),
        Err(e) => Err(e),
    }
}
