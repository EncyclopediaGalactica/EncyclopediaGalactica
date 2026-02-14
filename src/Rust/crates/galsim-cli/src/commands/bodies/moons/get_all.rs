use clap::Command;

pub fn get_all_moons_subcommand(moon_subcommand: Command) -> Command {
    moon_subcommand.subcommand(
        Command::new("list")
            .color(clap::ColorChoice::Always)
            .about(
                r#"
Listing the moons in the system. For more details see the --help command.
                "#,
            )
            .long_about(
                r#"
Listing the moons in the system long version. For more details see the --help command.
            "#,
            )
            .propagate_version(true)
            .arg_required_else_help(true),
    )
}
