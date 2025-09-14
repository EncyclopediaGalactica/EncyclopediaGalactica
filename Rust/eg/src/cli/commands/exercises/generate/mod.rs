use clap::Command;

use self::book::generate_book_subcommand;

pub mod book;

pub fn generate_command(root_command: Command) -> Command {
    let generate_subcommand = 
            Command::new("generate")
            .about("Generating exercises randomly in the given volume from the defined book.")
            .long_about(
                r#"
                Generating exercises randomly in the given volume from the defined book.
                The book needs to be referenced by its reference. The list of book references can be list by the
                `eg exercises list books` 
                command.
            "#)
            .propagate_version(true)
            .arg_required_else_help(true);
    let generate_subcommand = generate_book_subcommand(generate_subcommand);

    root_command.subcommand(generate_subcommand)
}
