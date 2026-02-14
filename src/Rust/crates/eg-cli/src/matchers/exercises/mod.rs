use anyhow::Ok;
use clap::ArgMatches;

use crate::logic::ExercisesConfig;

use self::generate::generate_subcommand_matchers;

pub mod book;
pub mod generate;
pub mod list;
pub mod sync;

pub async fn find_exercises_subcommand_matchers(
    arg_matches: ArgMatches,
    config: ExercisesConfig,
) -> anyhow::Result<()> {
    match arg_matches.subcommand() {
        Some(("generate", generate_matches)) => {
            generate_subcommand_matchers(generate_matches.clone(), config.clone()).await?;
            Ok(())
        }
        Some(("sync", sync_matches)) => {
            sync::sync(sync_matches.clone(), config.clone()).await?;
            Ok(())
        }
        // Some(exercises_matches) => {
        //     generate(exercises_matches.clone()).await?;
        //     sync::sync(exercises_matches.clone(), config.clone()).await?;
        //     Ok(())
        _ => Ok(()),
    }
}
