use clap::Command;
use clap::arg;
use clap::value_parser;

pub fn galaxy_subcommand(starmap_command: Command) -> Command {
    starmap_command.subcommand(
        Command::new("galaxy")
            .about("Galaxies related operations short version.")
            .long_about(
                "
                Galaxies related operations long version.
                ",
            )
            .propagate_version(true)
            .arg_required_else_help(true)
            .arg(
                arg!(-n --name <NAME> "Name of the galaxy..")
                    .required(true)
                    .value_parser(value_parser!(String)),
            ),
    )
}
