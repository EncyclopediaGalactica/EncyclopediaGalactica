use sqlx::PgPool;

use crate::get_connection;

use super::storage::get_all_from_storage;
use super::types::GetAllStarSystemsScenarioResult;

/// Gets all star systems from the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn get_all_star_systems_scenario(
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<Vec<GetAllStarSystemsScenarioResult>> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    let star_systems = get_all_from_storage(db_pool).await?;
    Ok(star_systems
        .into_iter()
        .map(GetAllStarSystemsScenarioResult::from)
        .collect())
}
