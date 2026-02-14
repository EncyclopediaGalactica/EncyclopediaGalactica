use galnav_objects::moon::scenario_entities::add_moon_scenario_input::AddMoonScenarioInput;
use galnav_objects::moon::scenario_entities::add_moon_scenario_result::AddMoonScenarioResult;
use galnav_storage::moon::add::add_moon;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_add_moon_scenario_input;

pub async fn add_moon_scenario(
    input: AddMoonScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<AddMoonScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_add_moon_scenario_input(input.clone()).await?;
    let recorded_moon = add_moon(AddMoonScenarioInput::into(input), db_pool).await?;
    Ok(AddMoonScenarioResult::from(recorded_moon))
}
