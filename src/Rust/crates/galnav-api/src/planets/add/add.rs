use galnav_objects::planet::scenario_entities::add_planet_scenario_input::AddPlanetScenarioInput;
use galnav_objects::planet::scenario_entities::add_planet_scenario_result::AddPlanetScenarioResult;
use gal_nav_repository::planet::add_planet::add_planet;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_add_planet_scenario_input;

/// Adds a planet to the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::AddPlanetScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn add_planet_scenario(
    input: AddPlanetScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<AddPlanetScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_add_planet_scenario_input(input.clone()).await?;
    let recorded_planet = add_planet(AddPlanetScenarioInput::into(input), db_pool).await?;
    Ok(AddPlanetScenarioResult::from(recorded_planet))
}
