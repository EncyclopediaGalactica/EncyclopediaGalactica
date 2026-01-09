use anyhow::Result;

use crate::logic::AppConfig;

use self::commands::build_cli;
use self::matchers::find_matches;

pub mod commands;
pub mod matchers;

pub async fn encyclopedia_galactica_cli(config: AppConfig) -> Result<()> {
    let cli_arg_matches = build_cli();
    find_matches(cli_arg_matches, config).await?;
    Ok(())
}
