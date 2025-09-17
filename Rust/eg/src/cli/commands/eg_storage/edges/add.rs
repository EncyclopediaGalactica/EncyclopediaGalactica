use clap::Command;
use clap::arg;
use clap::value_parser;

pub fn add_subcommand(edges_subcommand: Command) -> Command {
    edges_subcommand.subcommand(
        Command::new("add")
            .arg_required_else_help(true)
            .about("Add a new edge to the graph storage.")
            .long_about(
                "
                Add a new edge to the graph storage long version.
                ",
            )
            .arg(
                arg!(-t --type <TYPE_NAME> "The edge type")
                    .required(true)
                    .value_parser(value_parser!(String)),
            ),
    )
}
