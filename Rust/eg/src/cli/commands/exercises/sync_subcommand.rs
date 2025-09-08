use clap::Command;
use clap::arg;

pub fn sync_subcommand(exercises_command: Command) -> Command {
    exercises_command.subcommand(
        Command::new("sync")
            .about("Synchronises the exercises catalogue.")
            .long_about("Synchronises the exercises catalogue long versions.")
            .arg_required_else_help(true)
            .arg(
                arg!(-p --path <PATH> "Path to the exercises catalogue")
                    .long_help("Path to the exercise catalogue long.")
                    .required(true)
                    .value_parser(clap::value_parser!(String)),
            ),
    )
}
