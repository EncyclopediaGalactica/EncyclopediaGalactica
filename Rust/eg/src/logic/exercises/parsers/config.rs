use clap::Error;
use config::File;
use serde::Deserialize;

pub fn parse() -> anyhow::Result<Config, Error> {
    match config::Config::builder()
        .add_source(File::with_name("exercises.config.toml"))
        .build()
    {
        Ok(r) => match r.try_deserialize::<Config>() {
            Ok(res) => Ok(res),
            Err(e) => {
                panic!(
                    "Failed to parse the exercises.config.toml file. Error: {}",
                    e
                )
            }
        },
        Err(e) => {
            panic!(
                "Failed to parse the exercises.config.toml file. Error: {}",
                e
            )
        }
    }
}

#[derive(Deserialize)]
pub struct Config {
    database: Database,
}

impl std::fmt::Debug for Config {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        f.debug_struct("Config")
            .field("database", &self.database)
            .finish()
    }
}

impl Config {
    pub fn database(&self) -> &Database {
        &self.database
    }
}

#[derive(Debug, Deserialize)]
pub struct Database {
    pub url: String,
}

impl Database {
    pub fn url(&self) -> &str {
        &self.url
    }
}
