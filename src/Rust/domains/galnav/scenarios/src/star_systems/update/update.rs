use sqlx::PgPool;

use crate::get_connection;
use crate::star_systems::StarSystemEntity;

use super::storage::update_in_storage;
use super::types::{UpdateStarSystemScenarioInput, UpdateStarSystemScenarioResult};
use super::validation::validate_update_star_system_scenario_input;

/// Updates a star system in the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::UpdateStarSystemScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn update_star_system_scenario(
    input: UpdateStarSystemScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdateStarSystemScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_star_system_scenario_input(input.clone()).await?;
    let updated_star_system = update_in_storage(StarSystemEntity::from(input), db_pool).await?;
    Ok(UpdateStarSystemScenarioResult::from_entity(
        updated_star_system,
    ))
}
