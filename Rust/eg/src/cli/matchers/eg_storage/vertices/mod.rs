use clap::ArgMatches;

use crate::AppConfig;

use self::add::eg_storage_vertices_add_matcher;

pub mod add;

pub async fn eg_storage_vertices_matcher(
    args: ArgMatches,
    config: AppConfig,
) -> anyhow::Result<()> {
    match args.subcommand() {
        Some(("add", vertices_matches)) => {
            eg_storage_vertices_add_matcher(vertices_matches.clone(), config.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
