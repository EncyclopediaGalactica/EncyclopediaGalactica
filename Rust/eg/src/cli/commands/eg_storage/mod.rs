use clap::Command;

use self::edges::edges_subcommand;

pub mod edges;
pub fn build_eg_storage_cli(root_command: Command) -> Command {
    let storage_command = Command::new("eg-storage")
        .about("Encyclopedia Galactica storage related commands short versoin.")
        .long_about("Encyclopedia Galactica storage related commands long versoin.");
    let storage_command = edges_subcommand(storage_command);
    root_command.subcommand(storage_command)
}
