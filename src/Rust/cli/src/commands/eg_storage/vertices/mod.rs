use clap::Command;

pub mod add;

pub fn vertices_subcommand(eg_root_command: Command) -> Command {
    let vertices_subcommand = Command::new("vertices")
        .about("Vertices related operations in the graph storage short version..")
        .long_about(
            r#"
                Verticesrelated operations in the graph storage long version.
                "#,
        )
        .arg_required_else_help(true);
    let vertices_subcommand = add::add_subcommand(vertices_subcommand);
    eg_root_command.subcommand(vertices_subcommand)
}
