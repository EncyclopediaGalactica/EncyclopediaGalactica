use clap::Arg;
use clap::ArgAction;
use clap::Command;
use clap::value_parser;

pub fn add_command(dictionary_command: Command) -> Command {
    dictionary_command.subcommand(
        Command::new("add")
            .color(clap::ColorChoice::Always)
            .about(
                r#"
Adding a word, phrase or expression to the dictionary. For examples see the `--help` version.
            "#,
            )
            .long_about(
                r#"
Adding a word, phrase or expression to the dictionary.

This command will create a file under the $text_book_catalog/languages/{$language}/dictionary directory.
The file is a LaTeX file, meaning you can edit it and syncing it to the database afterwards.
This command line util intended to be a quick way to create these files and enabling you to
decorate them later as you see fit.

To avoid problems coming from using spaces in the expressions,
please wrap the values into double quotes.

**Example**:

`eg exercises languages dictionary add --language dutch --word "the word" --definition "the definition"`

`eg exercises languages dictionary add -l dutch -w "the word" -d "the definition"`
            "#,
            )
        .propagate_version(true)
        .arg_required_else_help(true)
        .arg(
                Arg::new("LANGUAGE")
                .short('l')
                    .long("language")
                    .help(r#"
The language of the word, phrase or expression.
                    "#)
                    .long_help(r#"
The language of the word, phrase or expression.
                    "#)
                    .required(true)
                    .action(ArgAction::Set)
                    .value_parser(value_parser!(String)),
            )
        .arg(
                Arg::new("WORD")
                .short('w')
                    .long("word")
                    .help(r#"
The word, phrase or expression in the target language.
                    "#)
                    .long_help(r#"
The word, phrase or expression in the target language.
To avoid problems coming from using spaces in the expressions,
please wrap the values into double quotes.
                    "#)
                    .required(true)
                    .action(ArgAction::Set)
                    .value_parser(value_parser!(String)),
            )
        .arg(
                Arg::new("DEFINITION")
                .short('d')
                    .long("definition")
                    .help(r#"
The meaning of the word, phrase or expression in the target language.
                    "#)
                    .long_help(r#"
The meaning of the word, phrase or expression in the target language.
To avoid problems coming from using spaces in the expressions,
please wrap the values into double quotes.
                    "#)
                    .required(true)
                    .action(ArgAction::Set)
                    .value_parser(value_parser!(String)),
            ),
    )
}
