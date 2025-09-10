use anyhow::Ok;
use cli::encyclopedia_galactica_cli;
use config::Config;
use config::File;
use serde::Deserialize;

pub mod cli;
pub mod logic;

#[tokio::main]
async fn main() -> anyhow::Result<()> {
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

#[derive(Deserialize, Clone, Debug)]
pub struct AppConfig {
    exercises: ExercisesConfig,
}

#[derive(Deserialize, Clone, Debug)]
pub struct ExercisesConfig {
    database_connection_string: String,
    catalog_path: String,
    generated_tests_path: String,
}
