use clap::ArgMatches;

use self::add::eg_storage_edges_add;

pub mod add;

pub async fn eg_storage_edges_matcher(args: ArgMatches) -> anyhow::Result<()> {
    println!("args: {:#?}", args);
    match args.subcommand() {
        Some(("add", edges_matches)) => {
            eg_storage_edges_add(edges_matches.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
