use clap::Arg;
use clap::ArgAction;
use clap::Command;
use clap::value_parser;

pub fn add_edges_type_command(edge_types_subcommand: Command) -> Command {
    edge_types_subcommand.subcommand(
        Command::new("add")
            .arg_required_else_help(true)
            .about("Add a new edge-type to the graph storage.")
            .long_about(
                "
                Add a new edge-type to the graph storage long version.
                ",
            )
            .arg(
                Arg::new("NAME")
                    .short('n')
                    .long("name")
                    .help("The name of the edge-type.")
                    .long_help("The name of the edge-type long version.")
                    .required(true)
                    .value_parser(value_parser!(String)),
            )
            .arg(
                Arg::new("DESC")
                    .short('d')
                    .long("description")
                    .help("The description of the edge-type.")
                    .long_help("The description of the edge-type long version.")
                    .required(true)
                    .value_parser(value_parser!(String)),
            )
            .arg(
                Arg::new("LOG LEVEL")
                    .short('l')
                    .long("log-level")
                    .help("Setting up the log levels. Only debug works now.")
                    .required(false)
                    .action(ArgAction::Set)
                    .value_parser(value_parser!(String)),
            ),
    )
}
