use galnav_objects::star::scenario_entities::update_star_scenario_input::UpdateStarScenarioInput;
use galnav_objects::star::scenario_entities::update_star_scenario_result::UpdateStarScenarioResult;
use gal_nav_repository::star::update_by_id::update_star_by_id;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_update_star_scenario_input;

pub async fn update_star_scenario(
    input: UpdateStarScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdateStarScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_star_scenario_input(input.clone()).await?;
    let updated_star = update_star_by_id(UpdateStarScenarioInput::into(input), db_pool).await?;
    Ok(UpdateStarScenarioResult::from(updated_star))
}
