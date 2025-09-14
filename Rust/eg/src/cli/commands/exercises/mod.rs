use clap::Command;

use self::generate::generate_command;

pub mod book_subcommand;
pub mod generate;
pub mod list;
pub mod sync_subcommand;

pub fn build_exercises_cli(root_command: Command) -> Command {
    let exercises_command = Command::new("exercises")
        .about("Exercises extension to generate test sets from the catalogised books.")
        .long_about(
            "
Exercises extension to generate test sets from the catalogised books.
",
        )
        .propagate_version(true)
        .arg_required_else_help(true);
    let exercises_command = generate_command(exercises_command);
    // let exercises_command = list_subcommand(exercises_command);
    let exercises_command = sync_subcommand::sync_subcommand(exercises_command);
    root_command.subcommand(exercises_command)
}
