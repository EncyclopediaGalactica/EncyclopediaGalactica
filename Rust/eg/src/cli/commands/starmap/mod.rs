use clap::Command;

use self::galaxy::galaxy_subcommand;

pub mod galaxy;

pub fn build_starmap_cli(eg_root_command: Command) -> Command {
    let starmap_command = Command::new("starmap")
        .about("Starmap cli short version.")
        .long_about("Starmap cli long version.");
    let starmap_command = galaxy_subcommand(starmap_command);
    eg_root_command.subcommand(starmap_command)
}
