use galsim_objects::planet::scenario_entities::get_all_planets_scenario_result::GetAllPlanetsScenarioResult;
use galsim_storage::planet::get_all::get_all_planets;
use sqlx::PgPool;

use crate::get_connection;

/// Retrieves all planets from the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn get_all_planets_scenario(
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<Vec<GetAllPlanetsScenarioResult>> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    let planets = get_all_planets(&db_pool).await?;
    Ok(planets
        .into_iter()
        .map(GetAllPlanetsScenarioResult::from)
        .collect())
}
