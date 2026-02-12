use gal_nav_domain_objects::moon::scenario_entities::delete_moon_scenario_input::DeleteMoonScenarioInput;
use gal_nav_repository::moon::delete_by_id::delete_moon_by_id;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_delete_moon_scenario_input;

pub async fn delete_moon_scenario(
    input: DeleteMoonScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<()> {
    validate_delete_moon_scenario_input(&input)?;
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    delete_moon_by_id(input.id, db_pool).await?;
    Ok(())
}
