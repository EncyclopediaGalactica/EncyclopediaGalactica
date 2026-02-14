use clap::Arg;
use clap::Command;
use clap::value_parser;

pub fn add_moon_subcommand(moon_subcommand: Command) -> Command {
    moon_subcommand.subcommand(
        Command::new("add")
            .color(clap::ColorChoice::Always)
            .about(
                r#"
Add a new moon to the system. For more details see the --help command.
                "#,
            )
            .long_about(
                r#"
Add a new moon to the system long version.
            "#,
            )
            .propagate_version(true)
            .arg_required_else_help(true)
            .arg(
                Arg::new("NAME")
                    .short('n')
                    .long("name")
                    .help("The name of the moon")
                    .long_help("The name of the moon long version")
                    .required(true)
                    .value_parser(value_parser!(String)),
            )
            .arg(
                Arg::new("DESCRIPTION")
                    .short('d')
                    .long("description")
                    .help("Additional information about the moon.")
                    .long_help("Additional information about the moon long version.")
                    .required(true)
                    .value_parser(value_parser!(String)),
            ),
    )
}
