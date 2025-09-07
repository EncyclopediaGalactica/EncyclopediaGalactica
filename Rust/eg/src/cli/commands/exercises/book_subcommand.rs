use clap::Command;
use clap::arg;
use clap::value_parser;

pub fn book_subcommand(commands: Command) -> Command {
    commands.subcommand(
        Command::new("book")
            .about(
                "
Getting the details of a book.
The particular book's reference must be provided and can be obtained by
'exercises list books'.
                    ",
            )
            .propagate_version(true)
            .arg_required_else_help(true)
            .arg(
                arg!(-r --reference <REF> "The reference of the book.")
                    .required(true)
                    .value_parser(value_parser!(String)),
            )
            .arg(
                arg!(-e --example <EX> "Just an example")
                    .required(false)
                    .value_parser(value_parser!(String)),
            ),
    )
}
