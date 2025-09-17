use clap::ArgMatches;

use crate::AppConfig;

use self::eg_storage::find_eg_storage_matchers;
use self::exercises::find_exercises_subcommand_matchers;

pub mod eg_storage;
pub mod exercises;
pub mod starmap;

pub async fn find_matches(arg_matches: ArgMatches, config: AppConfig) -> anyhow::Result<()> {
    match arg_matches.subcommand() {
        Some(("exercises", exercises_matches)) => {
            find_exercises_subcommand_matchers(exercises_matches.clone(), config.exercises.clone())
                .await?;
        }
        Some(("eg-storage", eg_storage_matches)) => {
            find_eg_storage_matchers(eg_storage_matches.clone());
        }
        _ => {}
    }
    Ok(())
}
