use anyhow::Result;
use clap::ArgMatches;
use clap::Command;

use self::commands::galsim_cli_commands;
use self::matchers::galsim_matchers;

pub mod commands;
pub mod matchers;

pub fn galsim_cli(cli_root_command: Command) -> Command {
    galsim_cli_commands(cli_root_command)
}

pub async fn find_galsim_matchers(args: ArgMatches) -> Result<()> {
    match args.subcommand() {
        Some(("bodies", bodies_matches)) => galsim_matchers(bodies_matches.clone()).await,
        _ => Ok(()),
    }
}
