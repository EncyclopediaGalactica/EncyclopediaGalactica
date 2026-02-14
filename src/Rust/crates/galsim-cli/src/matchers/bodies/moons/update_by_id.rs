use anyhow::Result;
use clap::ArgMatches;

pub async fn moons_update_by_id_matcher(_args: &ArgMatches) -> Result<()> {
    println!("moons_add_matcher we call the galsim-api moons update by id");
    Ok(())
}
