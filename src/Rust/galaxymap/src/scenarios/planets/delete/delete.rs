use sqlx::PgPool;

use crate::get_connection;

use super::storage::delete_from_storage;
use super::types::DeletePlanetScenarioInput;
use super::validation::validate_delete_planet_scenario_input;

/// Deletes a planet from the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::DeletePlanetScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn delete_planet_scenario(
    input: DeletePlanetScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<()> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_delete_planet_scenario_input(&input)?;
    delete_from_storage(&db_pool, input).await?;
    Ok(())
}
