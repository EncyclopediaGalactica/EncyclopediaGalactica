use clap::arg;
use clap::value_parser;
use clap::Command;

pub fn generate_book_command(root_command: Command) -> Command {
    root_command.subcommand(
            Command::new("generate")
            .about("Generating exercises from the provided topic, book and chapters.")
            .propagate_version(true)
            .arg_required_else_help(true)
                .subcommand(
                    Command::new("book")
                    .about("
When the exercise list generation is based on a single book.
When no volumes are provided for the question types the main volume will be
equally split among the types.
                        ")
                    .arg(
                        arg!(
                            --book <BOOK> 
                            "The reference of the book")
                        .required(true)
                        .value_parser(value_parser!(String)),
                    )
                    .arg(
                        arg!(
                            --chapters <CHAPTERS> 
                            "From which chapters the exercises going to be generated.")
                        .required(false)
                        .value_parser(value_parser!(String)),
                    )
                    .arg(
                        arg!(
                            --concept <VOLUME> 
                            "The volume of concept checking questions to be generated")
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        arg!(
                            --skill <VOLUME> 
                            "The volume of skill checking questions to be generated"
                        )
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        arg!(
                            --application <VOLUME> 
                            "The volume of application checking questions to be generated")
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        arg!(
                            --discussion <VOLUME> 
                            "The volume of questions where the question subject should be discussed")
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    ))
    )
}
