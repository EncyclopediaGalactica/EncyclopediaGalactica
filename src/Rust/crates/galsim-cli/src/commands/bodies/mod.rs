use clap::Command;

use self::moons::galsim_bodies_moon_commands;

pub mod moons;

pub fn galsim_bodies_commands(galsim_cli: Command) -> Command {
    let galsim_cli_bodies = Command::new("bodies")
        .propagate_version(true)
        .arg_required_else_help(true)
        .about(
            r#"
Working with celestial bodies in the galaxy.
        "#,
        )
        .long_about(
            r#"
Working with celestial bodies in the galaxy. Long version.
            "#,
        );

    let galsim_cli_bodies_moon = galsim_bodies_moon_commands(galsim_cli_bodies);

    galsim_cli.subcommand(galsim_cli_bodies_moon)
}
