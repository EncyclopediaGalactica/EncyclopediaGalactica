use anyhow::Result;
use clap::ArgMatches;

pub async fn moons_get_all_matcher(_args: &ArgMatches) -> Result<()> {
    println!("moons_add_matcher we call the galsim-api moons getall scenario");
    Ok(())
}
