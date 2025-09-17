use clap::Arg;
use clap::Command;
use clap::value_parser;

pub fn add_subcommand(edges_subcommand: Command) -> Command {
    edges_subcommand.subcommand(
        Command::new("add")
            .arg_required_else_help(true)
            .about("Add a new vertex to the graph storage.")
            .long_about(
                "
                Add a new vertex to the graph storage long version.
                ",
            )
            .arg(
                Arg::new("DATA")
                    .short('d')
                    .long("data")
                    .help("The data of the vertex")
                    .long_help("The data of the vertex")
                    .value_parser(value_parser!(String))
                    .required(false),
            ),
    )
}
