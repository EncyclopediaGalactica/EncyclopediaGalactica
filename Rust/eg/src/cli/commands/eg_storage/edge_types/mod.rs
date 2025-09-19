use clap::Command;

pub mod add;

pub fn edge_types_subcommand(eg_root_command: Command) -> Command {
    let edge_types_subcommand = Command::new("edge-types")
        .about("Edge-type related operations in the graph storage short version..")
        .long_about(
            r#"
                Edge-type related operations in the graph storage long version.
                "#,
        )
        .arg_required_else_help(true);
    let edge_types_subcommand = add::add_edges_type_command(edge_types_subcommand);
    eg_root_command.subcommand(edge_types_subcommand)
}
