pub mod add;
pub mod delete_by_id;
pub mod get_all;
pub mod update_by_id;

use anyhow::Result;
use clap::ArgMatches;

use self::add::moons_add_matcher;
use self::delete_by_id::moons_delete_by_id_matcher;
use self::get_all::moons_get_all_matcher;
use self::update_by_id::moons_update_by_id_matcher;

pub async fn galsim_bodies_moon_matchers(args: &ArgMatches) -> Result<()> {
    match args.subcommand() {
        Some(("add", add_subcommand)) => {
            moons_add_matcher(add_subcommand).await?;
            Ok(())
        }
        Some(("delete", delete_subcommand)) => {
            moons_delete_by_id_matcher(delete_subcommand).await?;
            Ok(())
        }
        Some(("get_all", get_all_subcommand)) => {
            moons_get_all_matcher(get_all_subcommand).await?;
            Ok(())
        }
        Some(("update", update_subcommand)) => {
            moons_update_by_id_matcher(update_subcommand).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
