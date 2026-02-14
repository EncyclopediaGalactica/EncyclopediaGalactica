use anyhow::Result;
use clap::ArgMatches;

use self::moons::galsim_bodies_moon_matchers;

pub mod moons;

pub async fn galsim_bodies_matchers(args: &ArgMatches) -> Result<()> {
    match args.subcommand() {
        Some(("moons", moons_matches)) => galsim_bodies_moon_matchers(&moons_matches).await,
        _ => Ok(()),
    }
}
