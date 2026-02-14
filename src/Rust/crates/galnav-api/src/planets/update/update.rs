use galnav_objects::planet::scenario_entities::update_planet_scenario_input::UpdatePlanetScenarioInput;
use galnav_objects::planet::scenario_entities::update_planet_scenario_result::UpdatePlanetScenarioResult;
use gal_nav_repository::planet::update_by_id::update_by_id;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_update_planet_scenario_input;

pub async fn update_planet_scenario(
    input: UpdatePlanetScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdatePlanetScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_planet_scenario_input(input.clone()).await?;
    let updated_planet = update_by_id(&db_pool, UpdatePlanetScenarioInput::into(input)).await?;
    Ok(UpdatePlanetScenarioResult::from_entity(updated_planet))
}
