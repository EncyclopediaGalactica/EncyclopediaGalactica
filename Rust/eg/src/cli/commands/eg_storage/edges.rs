use clap::Command;
use clap::arg;
use clap::value_parser;

pub fn edges_subcommand(eg_root_command: Command) -> Command {
    eg_root_command.subcommand(
        Command::new("edges")
            .about("Edge related operations in the graph storage short version..")
            .long_about(
                "
                Edge related operations in the graph storage long version.
                ",
            )
            .propagate_version(true)
            .arg_required_else_help(true)
            .arg(
                arg!(-t --type <TYPE_NAME> "The edge type")
                    .required(true)
                    .value_parser(value_parser!(String)),
            ),
    )
}
