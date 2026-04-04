use anyhow::Context;
use sqlx::PgPool;
use sqlx::postgres::PgPoolOptions;
use thiserror::Error;

pub mod distance;
pub mod extend;
pub mod moons;
pub mod planets;
pub mod star_systems;
pub mod stars;

#[derive(Debug, Error)]
#[error("StarMap Validation Error happened")]
pub struct StarMapValidationResult {
    pub is_valid: bool,
    pub message: String,
}

impl StarMapValidationResult {
    pub fn fail(message: String) -> Self {
        Self {
            is_valid: false,
            message,
        }
    }
    pub fn valid() -> Self {
        Self {
            is_valid: true,
            message: "".to_string(),
        }
    }
}

pub async fn get_connection(
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<PgPool> {
    match pg_pool {
        Some(pool) => Ok(pool),
        None => {
            let connection_string = db_connection_string
                .filter(|s| !s.is_empty())
                .map(|s| s.to_string())
                .or_else(|| std::env::var("DATABASE_URL").ok())
                .context("No database connection string found")?;

            let connection = PgPoolOptions::new()
                .max_connections(20)
                .connect(&connection_string)
                .await
                .unwrap_or_else(|e| panic!("Couldn't create database client. Error: {}", e));
            Ok(connection)
        }
    }
}
