use anyhow::Ok;
use clap::ArgMatches;

use crate::AppConfig;

pub mod edge_types;
pub mod edges;
pub mod vertices;

pub async fn find_eg_storage_matchers(args: ArgMatches, config: AppConfig) -> anyhow::Result<()> {
    match args.subcommand() {
        Some(("edges", edges_matches)) => {
            edges::eg_storage_edges_matcher(edges_matches.clone()).await?;
            Ok(())
        }
        Some(("edge-types", edge_types_matches)) => {
            edges::eg_storage_edges_matcher(edge_types_matches.clone()).await?;
            Ok(())
        }
        Some(("vertices", vertices_matches)) => {
            vertices::eg_storage_vertices_matcher(vertices_matches.clone(), config.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
