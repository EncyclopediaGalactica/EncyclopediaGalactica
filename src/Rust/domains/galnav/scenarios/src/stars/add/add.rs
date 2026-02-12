use gal_nav_domain_objects::star::scenario_entities::add_star_scenario_input::AddStarScenarioInput;
use gal_nav_domain_objects::star::scenario_entities::add_star_scenario_result::AddStarScenarioResult;
use gal_nav_repository::star::add::add_star;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_add_star_scenario_input;

/// Adds a star to the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::AddStarScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn add_star_scenario(
    input: AddStarScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<AddStarScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_add_star_scenario_input(input.clone()).await?;
    let recorded_star = add_star(AddStarScenarioInput::into(input), db_pool).await?;
    Ok(AddStarScenarioResult::from(recorded_star))
}
