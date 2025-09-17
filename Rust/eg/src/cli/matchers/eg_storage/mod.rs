use anyhow::Ok;
use clap::ArgMatches;

pub mod edges;
pub mod vertices;

pub async fn find_eg_storage_matchers(args: ArgMatches) -> anyhow::Result<()> {
    match args.subcommand() {
        Some(("edges", edges_matches)) => {
            edges::eg_storage_edges_matcher(edges_matches.clone()).await?;
            Ok(())
        }
        Some(("vertices", vertices_matches)) => {
            vertices::eg_storage_vertices_matcher(vertices_matches.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
