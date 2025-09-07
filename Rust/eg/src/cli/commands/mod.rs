use clap::ArgMatches;
use clap::command;

use self::eg_storage::build_eg_storage_cli;
use self::exercises::build_exercises_cli;
use self::starmap::build_starmap_cli;

pub mod eg_storage;
pub mod exercises;
pub mod starmap;

pub fn build_cli() -> ArgMatches {
    let root_command = command!()
        .about("Encyclopedia Galactica CLI")
        .long_about(
            "
Encyclopedia Galactica CLI.
This CLI provides access to Encyclopedia Galactica's storage directly and to the extensions it has.
The extensions are available as subcommands.
        ",
        )
        .propagate_version(true)
        .arg_required_else_help(true);
    let root_command = build_exercises_cli(root_command);
    let root_command = build_eg_storage_cli(root_command);
    let root_command = build_starmap_cli(root_command);
    root_command.get_matches()
}
