use sqlx::PgPool;

use crate::get_connection;

use super::storage::get_all_from_storage;
use super::types::{GetAllMoonsScenarioInput, GetAllMoonsScenarioResult};

/// Gets all moons from the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::GetAllMoonsScenarioInput` - The input data for the scenario (empty)
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn get_all_moons_scenario(
    _input: GetAllMoonsScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<Vec<GetAllMoonsScenarioResult>> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    let all_moons = get_all_from_storage(db_pool).await?;
    Ok(all_moons.into_iter().map(GetAllMoonsScenarioResult::from).collect())
}