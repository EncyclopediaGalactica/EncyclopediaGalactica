use sqlx::Pool;
use sqlx::Postgres;
use sqlx::postgres::PgPoolOptions;
use thiserror::Error;

pub mod repository;
pub mod scenarios;

#[derive(Debug, Error)]
#[error("StarMap Validation Error happened")]
pub struct StarMapValidationResult {
    is_valid: bool,
    message: String,
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

pub async fn get_connection(connection_string: &str) -> anyhow::Result<Pool<Postgres>> {
    let connection = PgPoolOptions::new()
        .max_connections(5)
        .connect(&connection_string)
        .await
        .unwrap_or_else(|e| panic!("Couldn't create database client. Error: {}", e));
    Ok(connection)
}
