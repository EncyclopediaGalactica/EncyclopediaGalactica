use galnav_objects::star_system::entities::star_system::StarSystemEntity;
use galnav_objects::star_system::scenario_entities::update_star_system_scenario_input::UpdateStarSystemScenarioInput;
use galnav_objects::star_system::scenario_entities::update_star_system_scenario_result::UpdateStarSystemScenarioResult;
use galnav_storage::star_system::update_by_id::update_by_id;
use sqlx::PgPool;

use super::validation::validate_update_star_system_scenario_input;
use crate::get_connection;

pub async fn update_star_system_scenario(
    input: UpdateStarSystemScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdateStarSystemScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_star_system_scenario_input(input.clone()).await?;
    let updated_star_system = update_by_id(StarSystemEntity::from(input), db_pool).await?;
    Ok(UpdateStarSystemScenarioResult::from_entity(
        updated_star_system,
    ))
}
