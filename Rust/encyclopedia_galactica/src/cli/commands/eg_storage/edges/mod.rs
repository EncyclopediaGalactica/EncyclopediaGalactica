use clap::Command;

pub mod add;

pub fn edges_subcommand(eg_root_command: Command) -> Command {
    let edges_subcommand = Command::new("edges")
        .about("Edge related operations in the graph storage short version.")
        .long_about(
            "
                Edge related operations in the graph storage long version.
                ",
        )
        .arg_required_else_help(true);
    let edges_subcommand = add::add_subcommand(edges_subcommand);
    eg_root_command.subcommand(edges_subcommand)
}
