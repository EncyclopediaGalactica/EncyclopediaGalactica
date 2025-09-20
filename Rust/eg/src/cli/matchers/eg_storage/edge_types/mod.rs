use clap::ArgMatches;

use crate::AppConfig;

use self::add::eg_storage_edge_types_add;

pub mod add;

pub async fn eg_storage_edge_types_matcher(
    args: ArgMatches,
    config: AppConfig,
) -> anyhow::Result<()> {
    match args.subcommand() {
        Some(("add", edges_matches)) => {
            eg_storage_edge_types_add(edges_matches.clone(), config.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
