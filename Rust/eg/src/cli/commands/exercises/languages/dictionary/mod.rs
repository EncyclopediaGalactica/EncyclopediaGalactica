use clap::Command;

use self::add::add_command;

mod add;

pub fn dictionary_command(root_command: Command) -> Command {
    let dictionary_command = Command::new("dictionary")
        .color(clap::ColorChoice::Always)
        .about(
            r#"
Working with words, phrases and expressions from the given language.
"#,
        )
        .long_about(
            r#"
Working with words, phrases and expressions from the given language.
            "#,
        )
        .propagate_version(true)
        .arg_required_else_help(true);

    let dictionary_command = add_command(dictionary_command);

    root_command.subcommand(dictionary_command)
}
