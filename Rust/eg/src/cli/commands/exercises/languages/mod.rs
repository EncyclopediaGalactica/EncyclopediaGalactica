use clap::Command;

use self::dictionary::dictionary_command;

mod dictionary;

pub fn languages_command(root_command: Command) -> Command {
    let languages_subcommand = Command::new("languages")
        .color(clap::ColorChoice::Always)
        .about(r#"The human languages domain of the Exercises module."#)
        .long_about(
            r#"
The human languages domain of the Exercises module.
            "#,
        )
        .propagate_version(true)
        .arg_required_else_help(true);

    let languages_subcommand = dictionary_command(languages_subcommand);

    root_command.subcommand(languages_subcommand)
}
