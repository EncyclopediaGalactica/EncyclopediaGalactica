use clap::Command;

use self::bodies::galsim_bodies_commands;

pub mod bodies;

pub fn galsim_cli_commands(eg_root_command: Command) -> Command {
    let galsim_cli = Command::new("galsim")
        .arg_required_else_help(true)
        .arg_required_else_help(true)
        .color(clap::ColorChoice::Always)
        .about(
            r#"
Galaxy Navigator module for Encyclopedia Galactica.
        "#,
        )
        .long_about(
            r#"
Galaxy Navigator module for Encyclopedia Galactica.
Long version
        "#,
        );

    let galsim_cli = galsim_bodies_commands(galsim_cli);
    eg_root_command.subcommand(galsim_cli)
}
