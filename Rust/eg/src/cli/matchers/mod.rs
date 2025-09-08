use clap::ArgMatches;

use self::eg_storage::find_eg_storage_matchers;
use self::exercises::find_exercises_matchers;

pub mod eg_storage;
pub mod exercises;
pub mod starmap;

pub async fn find_matches(arg_matches: ArgMatches) {
    find_eg_storage_matchers(arg_matches.clone());
    find_exercises_matchers(arg_matches.clone()).await;
}
