use clap::ArgMatches;
use log::LevelFilter;

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
            find_eg_storage_matchers(eg_storage_matches.clone(), config.clone()).await?;
        }
        _ => {}
    }
    Ok(())
}
pub fn set_cli_logging_level(args: ArgMatches) -> anyhow::Result<LevelFilter> {
    let log_level = match args
        .get_one::<String>("LOG LEVEL")
        .map(|s| s.as_str())
        .unwrap_or("off")
    {
        "off" => log::LevelFilter::Off,
        "error" => log::LevelFilter::Error,
        "warn" => log::LevelFilter::Warn,
        "info" => log::LevelFilter::Info,
        "debug" => log::LevelFilter::Debug,
        "trace" => log::LevelFilter::Trace,
        _ => log::LevelFilter::Info,
    };
    Ok(log_level)
}
