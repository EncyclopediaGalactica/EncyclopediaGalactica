use clap::ArgMatches;

use self::add::eg_storage_vertices_add;

pub mod add;

pub async fn eg_storage_vertices_matcher(args: ArgMatches) -> anyhow::Result<()> {
    match args.subcommand() {
        Some(("add", vertices_matches)) => {
            eg_storage_vertices_add(vertices_matches.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
