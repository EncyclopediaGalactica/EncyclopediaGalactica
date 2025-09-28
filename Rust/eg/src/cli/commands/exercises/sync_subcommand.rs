use clap::Arg;
use clap::ArgAction;
use clap::Command;
use clap::value_parser;

pub fn sync_subcommand(exercises_command: Command) -> Command {
    exercises_command.subcommand(
        Command::new("sync")
            .about("Synchronises the exercises catalogue. Read the --help version!")
            .long_about("
Synchronises the exercises catalogue long versions.
These operation is going to truncate all tables in the system by default meaning that all data will be lost.
Don't use this command if you don't know what you are doing.
Always read the --help version before using this command.
")
            .arg_required_else_help(true)
            .arg(
                Arg::new("full-overwrite")
                    .short('f')
                    .long("full-overwrite")
                    .help("Overwrites the whole catalogue. Read the --help version!")
                    .long_help("
Full overwrites the whole catalogue.
This operation is going to truncate all tables in the system by default meaning that all data will be lost.
")
                    .action(ArgAction::SetTrue),
            )
            .arg(
                Arg::new("LOG LEVEL")
                    .short('l')
                    .long("log-level")
                    .help("Setting up the log levels. Only debug works now.")
                    .required(false)
                    .action(ArgAction::Set)
                    .value_parser(value_parser!(String)),
            )
    )
}
