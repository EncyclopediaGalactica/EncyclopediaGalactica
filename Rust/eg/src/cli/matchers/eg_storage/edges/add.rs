use clap::ArgMatches;

pub async fn eg_storage_edges_add(args: ArgMatches) -> anyhow::Result<()> {
    match args.subcommand_matches("add") {
        Some(add_matches) => {
            println!("eg eg-storage add");
            Ok(())
        }
        None => Ok(()),
    }
}
