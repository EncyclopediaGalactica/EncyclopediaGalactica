use sqlx::PgPool;

use crate::get_connection;
use crate::star_systems::StarSystemEntity;

use super::storage::add_to_storage;
use super::types::{AddStarSystemScenarioInput, AddStarSystemScenarioResult};
use super::validation::validate_add_star_system_scenario_input;

/// Adds a star system to the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::AddStarSystemScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn add_star_system_scenario(
    input: AddStarSystemScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<AddStarSystemScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_add_star_system_scenario_input(input.clone()).await?;
    let recorded_star_system = add_to_storage(StarSystemEntity::from(input), db_pool).await?;
    Ok(AddStarSystemScenarioResult::from(recorded_star_system))
}
