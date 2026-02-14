use clap::ArgMatches;
use clap::command;

use galsim_cli::galsim_cli;

pub mod eg_storage;
pub mod exercises;
pub mod starmap;

pub fn build_cli() -> ArgMatches {
    let root_command = command!("eg")
        .about(r#"Encyclopedia Galactica CLI"#)
        .long_about(
            r#"
         Encyclopedia Galactica CLI.
         This CLI provides access to Encyclopedia Galactica's storage directly and to the extensions it has.
         The extensions are available as subcommands.
        "#,
        )
        .color(clap::ColorChoice::Always)
        .propagate_version(true)
        .arg_required_else_help(true);
    // let root_command = build_exercises_cli(root_command);
    // let root_command = build_eg_storage_cli(root_command);
    let root_command = galsim_cli(root_command);
    // let root_command = build_starmap_cli(root_command);
    root_command.get_matches()
}
