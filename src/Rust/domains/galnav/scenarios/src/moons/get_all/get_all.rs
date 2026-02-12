use gal_nav_domain_objects::moon::scenario_entities::get_all_moons_scenario_input::GetAllMoonsScenarioInput;
use gal_nav_domain_objects::moon::scenario_entities::get_all_moons_scenario_result::GetAllMoonsScenarioResult;
use gal_nav_repository::moon::get_all::get_all_moons;
use sqlx::PgPool;

use crate::get_connection;

pub async fn get_all_moons_scenario(
    _input: GetAllMoonsScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<Vec<GetAllMoonsScenarioResult>> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    let all_moons = get_all_moons(db_pool).await?;
    Ok(all_moons
        .into_iter()
        .map(GetAllMoonsScenarioResult::from)
        .collect())
}
