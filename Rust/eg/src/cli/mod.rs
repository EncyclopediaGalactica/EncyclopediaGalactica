use self::commands::build_cli;
use self::matchers::find_matches;

pub mod commands;
pub mod matchers;

pub async fn encyclopedia_galactica_cli() {
    let cli = build_cli();
    find_matches(cli).await;
}
