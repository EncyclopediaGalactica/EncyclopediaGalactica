use anyhow::Ok;
use anyhow::Result;
use clap::ArgMatches;

use self::bodies::galsim_bodies_matchers;

pub mod bodies;

pub async fn galsim_matchers(args: ArgMatches) -> Result<()> {
    match args.subcommand() {
        Some(("bodies", bodies_matches)) => galsim_bodies_matchers(bodies_matches).await,
        _ => Ok(()),
    }
}
