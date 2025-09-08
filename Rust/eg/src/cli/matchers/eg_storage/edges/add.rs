use clap::ArgMatches;

pub fn add(args: ArgMatches) {
    match args.subcommand_matches("add") {
        Some(add_matches) => {
            println!("eg eg-storage add");
        }
        None => {}
    }
}
