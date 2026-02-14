use clap::Arg;
use clap::Command;
use clap::value_parser;

pub fn update_moon_by_id_subcommand(moon_subcommand: Command) -> Command {
    moon_subcommand.subcommand(
        Command::new("update_by_id")
            .color(clap::ColorChoice::Always)
            .about(
                r#"
Updating a moon by its id. For more details see the --help command.
                "#,
            )
            .long_about(
                r#"
Updating a moon by its id long version. For more details see the --help command.
            "#,
            )
            .propagate_version(true)
            .arg_required_else_help(true)
            .arg(
                Arg::new("ID")
                    .long("id")
                    .help("The id of the moon.")
                    .long_help("The id of the moon long version")
                    .required(true)
                    .value_parser(value_parser!(usize)),
            )
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
