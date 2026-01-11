use anyhow::Ok;
use cli::encyclopedia_galactica_cli;
use config::Config;
use config::File;

use self::logic::AppConfig;

pub mod cli;
pub mod logic;

#[tokio::main]
pub async fn main() -> anyhow::Result<()> {
    let config = load_config()?;
    encyclopedia_galactica_cli(config).await?;
    Ok(())
}

fn load_config() -> anyhow::Result<AppConfig> {
    let config = Config::builder()
        .add_source(File::with_name("eg.config.toml").required(true))
        .build()?
        .try_deserialize::<AppConfig>()?;
    Ok(config)
}

pub async fn encyclopedia_galactica_cli(config: AppConfig) -> Result<()> {
    let cli_arg_matches = build_cli();
    find_matches(cli_arg_matches, config).await?;
    Ok(())
}
