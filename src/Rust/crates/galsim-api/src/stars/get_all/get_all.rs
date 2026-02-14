use galsim_objects::star::scenario_entities::get_all_scenario_input::GetAllStarsScenarioInput;
use galsim_objects::star::scenario_entities::get_all_scenario_result::GetAllStarsScenarioResult;
use galsim_storage::star::get_all::get_all_stars;
use sqlx::PgPool;

use crate::get_connection;

/// Gets all stars from the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
pub async fn get_all_stars_scenario(
    _input: GetAllStarsScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<Vec<GetAllStarsScenarioResult>> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    let all_stars = get_all_stars(db_pool).await?;
    Ok(all_stars
        .into_iter()
        .map(GetAllStarsScenarioResult::from)
        .collect())
}
