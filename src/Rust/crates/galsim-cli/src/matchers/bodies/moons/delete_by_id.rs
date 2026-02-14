use anyhow::Result;
use clap::ArgMatches;

pub async fn moons_delete_by_id_matcher(args: &ArgMatches) -> Result<()> {
    println!("moons_add_matcher we call the galsim-api moons delete by id");
    Ok(())
}
