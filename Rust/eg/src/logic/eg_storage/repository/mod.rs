use sqlx::Pool;
use sqlx::Postgres;
use sqlx::postgres::PgPoolOptions;

pub async fn get_connection(connection_string: &str) -> anyhow::Result<Pool<Postgres>> {
    let connection = PgPoolOptions::new()
        .max_connections(5)
        .connect(&connection_string)
        .await
        .unwrap_or_else(|e| panic!("Couldn't create database client. Error: {}", e));
    Ok(connection)
}
