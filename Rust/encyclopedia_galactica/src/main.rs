use anyhow::Ok;
use cli::encyclopedia_galactica_cli;
use config::Config;
use config::File;
use encyclopedia_galactica::logic::AppConfig;
use serde::Deserialize;

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
