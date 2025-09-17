use clap::ArgMatches;

use crate::ExercisesConfig;

use self::book::exercises_generate_book_matchers;
pub mod book;

pub async fn generate_subcommand_matchers(
    arg_matches: ArgMatches,
    config: ExercisesConfig,
) -> anyhow::Result<()> {
    match arg_matches.subcommand() {
        Some(("book", book_arguments)) => {
            exercises_generate_book_matchers(book_arguments.clone(), config.clone()).await?;
            Ok(())
        }
        _ => Ok(()),
    }
}
