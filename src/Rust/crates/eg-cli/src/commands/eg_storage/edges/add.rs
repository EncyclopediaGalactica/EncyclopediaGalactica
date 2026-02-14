use clap::Arg;
use clap::ArgAction;
use clap::Command;
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
                Arg::new("NAME")
                    .short('n')
                    .long("--name")
                    .help("Name of the edge.")
                    .long_help("Name of the edge expresses what it represents")
                    .required(false)
                    .value_parser(value_parser!(String)),
            )
            .arg(
                Arg::new("FROM")
                    .long("--from")
                    .help("The id of the FROM vertex.")
                    .long_help("The id of the FROM vertex long version.")
                    .required(false)
                    .value_parser(value_parser!(i64)),
            )
            .arg(
                Arg::new("TO")
                    .long("--to")
                    .help("The id of the TO vertex.")
                    .long_help("The id of the TO vertex long version.")
                    .required(false)
                    .value_parser(value_parser!(i64)),
            )
            .arg(
                Arg::new("TYPE")
                    .short('t')
                    .long("--type")
                    .help("The id of the Edge type.")
                    .long_help("The id of the edge type long version.")
                    .required(false)
                    .value_parser(value_parser!(i64)),
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
