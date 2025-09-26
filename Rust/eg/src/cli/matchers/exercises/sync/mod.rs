use anyhow::Result;
use clap::ArgMatches;
use log::debug;
use tabled::builder::Builder;
use tabled::settings::Style;

use crate::ExercisesConfig;
use crate::cli::matchers::set_cli_logging_level;
use crate::logic::exercises::scenarios::sync::sync_catalog_to_db;

pub async fn sync(matches: ArgMatches, config: ExercisesConfig) -> Result<()> {
    println!("Matches: {:#?}", matches);
    let log_level =
        set_cli_logging_level(matches.clone()).unwrap_or_else(|_| log::LevelFilter::Off);
    println!("Logging level set to: {:?}", log_level);
    env_logger::Builder::new().filter(None, log_level).init();
    debug!("Logging level set to: {:?}", log_level);

    if matches.get_flag("full-overwrite") {
        match sync_catalog_to_db::sync_catalog_to_db(config.clone()).await {
            Ok(_) => {
                let mut table_builder = Builder::with_capacity(2, 2);
                table_builder
                    .push_record(["Operation:", "Sync catalog to database by overwriting it."]);
                table_builder.push_record(["Result:", "Success"]);

                let mut result_table = table_builder.build();
                result_table.with(Style::modern());
                println!("{}", result_table);
            }
            Err(e) => println!("Catalog sync failed: {}", e),
        }
    }
    Ok(())
}
