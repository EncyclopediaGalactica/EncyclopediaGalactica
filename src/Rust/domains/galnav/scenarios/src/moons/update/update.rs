use sqlx::PgPool;

use crate::get_connection;
use crate::moons::MoonEntity;

use super::storage::update_in_storage;
use super::types::{UpdateMoonScenarioInput, UpdateMoonScenarioResult};
use super::validation::validate_update_moon_scenario_input;

/// Updates a moon in the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::UpdateMoonScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn update_moon_scenario(
    input: UpdateMoonScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdateMoonScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_moon_scenario_input(input.clone()).await?;
    let updated_moon = update_in_storage(MoonEntity::from(input), db_pool).await?;
    Ok(UpdateMoonScenarioResult::from_entity(updated_moon))
}
