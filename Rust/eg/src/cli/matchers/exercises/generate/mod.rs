use clap::ArgMatches;

pub async fn generate(arg_matches: ArgMatches) -> anyhow::Result<()> {
    match arg_matches.subcommand() {
        Some(("book", book_arguments)) => {
            generate_book(book_arguments.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}

async fn generate_book(args: ArgMatches) -> anyhow::Result<()> {
    println!("generate book: {:#?}", args);
    Ok(())
}
