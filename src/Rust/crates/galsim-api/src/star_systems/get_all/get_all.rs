use galsim_objects::star_system::scenario_entities::get_all_star_systems_scenario_result::GetAllStarSystemsScenarioResult;
use galsim_storage::star_system::get_all::get_all_star_systems;
use sqlx::PgPool;

use crate::get_connection;

pub async fn get_all_star_systems_scenario(
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<Vec<GetAllStarSystemsScenarioResult>> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    println!("=== before the query");
    let star_systems = get_all_star_systems(db_pool).await?;
    Ok(star_systems
        .into_iter()
        .map(GetAllStarSystemsScenarioResult::from)
        .collect())
}
