use clap::Arg;
use clap::ArgAction;
use clap::Command;
use clap::arg;
use clap::value_parser;

pub fn generate_book_subcommand(generate_subcommand: Command) -> Command {
    generate_subcommand
                .subcommand(
                    Command::new("book")
                    .about(r#"
                    Exercises will be generated randomly from the defined book.
                        "#)
                   .long_about(r#"
                   Exercises will be generated randomly from the defined book.
                   The book needs to be referenced by its reference. The list of book references can be list by the
                   `eg exercises list books` 
                   command.
                   "#
                )
                    .propagate_version(true)
                    .arg_required_else_help(true)
                    .arg(
                        Arg::new("BOOKS")
                            .short('b')
                            .long("books")
                            .help("The reference of the book")
                            .long_help("long help")
                            .required(true)
                            .value_parser(value_parser!(String))
                    )
                    .arg(
                        Arg::new("CHAPTERS")
                            .short('c')
                            .long("chapters")
                            .help("From which chapters the exercises going to be generated. See --help for more details.")
                            .long_help(r#"
                            From which chapters the exercises going to be generated.
                            The chapters must be referenced by their reference names.
                            When multiple chapters are given they must be command separated.
                            When no chapters were provided the available chapters will be used.
                            The list of chapters and their references can be seen in the 
                            `eg exercises list chapters`
                            command.
                            "#)
                            .required(false)
                            .action(ArgAction::Set)
                            .value_parser(value_parser!(String)),
                    )
                    .arg(
                        Arg::new("CONCEPT QUESTIONS VOLUME")
                            .long("concept-questions")
                            .help("The volume of concept checking questions to be generated")
                            .long_help("long help")
                            .required(false)
                            .action(ArgAction::Set)
                            .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        Arg::new("SKILL QUESTIONS VOLUME")
                            .long("skill-questions")
                            .help("The volume of skill checking questions to be generated")
                            .long_help("long help")
                            .required(false)
                            .action(ArgAction::Set)
                            .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        Arg::new("APPLICATION QUESTIONS VOLUME")
                            .long("application-questions")
                            .help("The volume of application checking questions to be generated")
                            .long_help("long help")
                            .required(false)
                            .action(ArgAction::Set)
                            .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        Arg::new("DISCUSSION QUESTIONS VOLUME")
                            .long("discussion-questions")
                            .help("The volume of application checking questions to be generated")
                            .long_help("long help")
                            .required(false)
                            .action(ArgAction::Set)
                            .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        Arg::new("LOG LEVEL")
                            .short('l')
                            .long("log-level")
                            .help("log level")
                            .long_help("long help")
                            .required(false)
                            .action(ArgAction::Set)
                            .value_parser(value_parser!(String)),
                    ))
}
