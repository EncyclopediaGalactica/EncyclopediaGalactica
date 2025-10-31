use clap::ArgMatches;

use crate::ExercisesConfig;

use self::add::exercises_languages_add_matchers;

pub mod add;

pub async fn languages_subcommand_matchers(
    arg_matches: ArgMatches,
    config: ExercisesConfig,
) -> anyhow::Result<()> {
    match arg_matches.subcommand() {
        Some(("add", add_arguments)) => {
            exercises_languages_add_matchers(add_arguments.clone(), config.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
