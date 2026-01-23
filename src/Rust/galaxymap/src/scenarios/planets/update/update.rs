use sqlx::PgPool;

use crate::get_connection;

use super::storage::update_in_storage;
use super::types::{UpdatePlanetScenarioInput, UpdatePlanetScenarioResult};
use super::validation::validate_update_planet_scenario_input;

/// Updates a planet in the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::UpdatePlanetScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn update_planet_scenario(
    input: UpdatePlanetScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdatePlanetScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_planet_scenario_input(input.clone()).await?;
    let updated_planet = update_in_storage(&db_pool, input).await?;
    Ok(UpdatePlanetScenarioResult::from_entity(updated_planet))
}

#[cfg(test)]
mod tests {

    // Placeholder for unit tests - integration tests will cover the full flow
    #[test]
    fn test_placeholder() {
        assert!(true);
    }
}
