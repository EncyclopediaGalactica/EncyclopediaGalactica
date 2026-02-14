use anyhow::Ok;
use config::Config;
use config::File;
use eg_config::AppConfig;

use self::commands::build_cli;
use self::matchers::find_matches;

pub mod commands;
pub mod matchers;

#[tokio::main]
pub async fn main() -> anyhow::Result<()> {
    // let config = load_config()?;
    encyclopedia_galactica_cli().await?;
    Ok(())
}

fn load_config() -> anyhow::Result<AppConfig> {
    let config = Config::builder()
        .add_source(File::with_name("eg.config.toml").required(true))
        .build()?
        .try_deserialize::<AppConfig>()?;
    Ok(config)
}

pub async fn encyclopedia_galactica_cli() -> anyhow::Result<()> {
    let cli_arg_matches = build_cli();
    find_matches(cli_arg_matches).await?;
    Ok(())
}
