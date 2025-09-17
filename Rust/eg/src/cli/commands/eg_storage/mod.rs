use self::edges::edges_subcommand;
use self::vertices::vertices_subcommand;
use clap::Command;

pub mod edges;
pub mod vertices;

pub fn build_eg_storage_cli(root_command: Command) -> Command {
    let storage_command = Command::new("eg-storage")
        .about("Encyclopedia Galactica storage related commands short versoin.")
        .long_about("Encyclopedia Galactica storage related commands long versoin.")
        .arg_required_else_help(true);
    let storage_command = edges_subcommand(storage_command);
    let storage_command = vertices_subcommand(storage_command);
    root_command.subcommand(storage_command)
}
