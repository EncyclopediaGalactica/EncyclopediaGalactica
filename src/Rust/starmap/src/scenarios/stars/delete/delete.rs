use sqlx::PgPool;

use crate::get_connection;

use super::storage::delete_from_storage;
use super::types::DeleteStarScenarioInput;

/// Deletes a star from the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::DeleteStarScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn delete_star_scenario(
    input: DeleteStarScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<()> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    delete_from_storage(input.id, db_pool).await?;
    Ok(())
}