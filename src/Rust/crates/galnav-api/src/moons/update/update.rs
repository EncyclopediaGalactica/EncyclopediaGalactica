use galnav_objects::moon::scenario_entities::update_moon_scenario_input::UpdateMoonScenarioInput;
use galnav_objects::moon::scenario_entities::update_moon_scenario_result::UpdateMoonScenarioResult;
use galnav_storage::moon::update_by_id::update_moon_by_id;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_update_moon_scenario_input;

pub async fn update_moon_scenario(
    input: UpdateMoonScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdateMoonScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_moon_scenario_input(input.clone()).await?;
    let updated_moon = update_moon_by_id(UpdateMoonScenarioInput::into(input), db_pool).await?;
    Ok(UpdateMoonScenarioResult::from_entity(updated_moon))
}
