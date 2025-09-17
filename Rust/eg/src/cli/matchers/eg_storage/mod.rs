use clap::ArgMatches;

pub mod edges;

pub fn find_eg_storage_matchers(args: ArgMatches) {
    match args.subcommand_matches("eg-storage") {
        Some(eg_storage_matches) => {
            edges::edges(eg_storage_matches.clone());
        }
        None => {}
    }
}
