use anyhow::Result;
use clap::ArgMatches;

use crate::ExercisesConfig;
use crate::logic::exercises::scenarios::sync::sync_catalog_to_db;

pub async fn sync(matches: ArgMatches, config: ExercisesConfig) -> Result<()> {
    if matches.get_flag("full-overwrite") {
        sync_catalog_to_db::sync_catalog_to_db(config.clone()).await?
    }
    Ok(())
}
