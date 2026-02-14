use clap::Arg;
use clap::Command;
use clap::value_parser;

pub fn delete_moon_by_id_subcommand(moon_subcommand: Command) -> Command {
    moon_subcommand.subcommand(
        Command::new("delete")
            .color(clap::ColorChoice::Always)
            .about(
                r#"
Delete a moon by its id. For more details see the --help command.
                "#,
            )
            .long_about(
                r#"
Delete a moon by its id long version. For more details see the --help command.
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
            ),
    )
}
