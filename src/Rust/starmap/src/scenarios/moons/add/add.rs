use sqlx::PgPool;

use crate::get_connection;
use crate::scenarios::moons::MoonEntity;

use super::storage::add_to_storage;
use super::types::{AddMoonScenarioInput, AddMoonScenarioResult};
use super::validation::validate_add_moon_scenario_input;

/// Adds a moon to the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::AddMoonScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn add_moon_scenario(
    input: AddMoonScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<AddMoonScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_add_moon_scenario_input(input.clone()).await?;
    let recorded_moon = add_to_storage(MoonEntity::from(input), db_pool).await?;
    Ok(AddMoonScenarioResult::from(recorded_moon))
}