use clap::ArgMatches;

use self::generate::generate;

pub mod book;
pub mod generate;
pub mod list;
pub mod sync;

pub async fn find_exercises_matchers(arg_matches: ArgMatches) {
    match arg_matches.subcommand_matches("exercises") {
        Some(exercises_matches) => {
            generate(arg_matches.clone()).await;
            sync::sync(arg_matches.clone()).await;
        }
        None => {}
    }
}
