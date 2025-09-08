use clap::ArgMatches;

pub async fn generate(arg_matches: ArgMatches) {
    match arg_matches.subcommand() {
        Some(("book", book_arguments)) => {
            generate_book(book_arguments.clone()).await;
        }
        _ => {}
    }
}

async fn generate_book(args: ArgMatches) {}
