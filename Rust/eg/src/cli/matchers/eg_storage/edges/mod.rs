use clap::ArgMatches;

pub mod add;

pub fn edges(args: ArgMatches) {
    match args.subcommand_matches("edges") {
        Some(edges_matches) => add::add(edges_matches.clone()),
        None => {}
    }
}
