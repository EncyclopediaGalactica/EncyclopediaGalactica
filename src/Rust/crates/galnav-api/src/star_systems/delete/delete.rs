use galnav_objects::star_system::scenario_entities::delete_star_system_scenario_input::DeleteStarSystemScenarioInput;
use gal_nav_repository::star_system::delete_by_id::delete_star_system_by_id;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_delete_star_system_scenario_input;

pub async fn delete_star_system_scenario(
    input: DeleteStarSystemScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<()> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_delete_star_system_scenario_input(input.clone()).await?;
    delete_star_system_by_id(input.id, db_pool).await?;
    Ok(())
}
