use sqlx::PgPool;

use crate::get_connection;
use crate::scenarios::stars::StarEntity;

use super::storage::update_in_storage;
use super::types::{UpdateStarScenarioInput, UpdateStarScenarioResult};
use super::validation::validate_update_star_scenario_input;

/// Updates a star in the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
///
/// # Arguments
/// * `input::UpdateStarScenarioInput` - The input data for the scenario
/// * `pg_pool::Option<PgPool>` - The Postgres connection pool
/// * `db_connection_string::Option<&str>` - The database connection string
pub async fn update_star_scenario(
    input: UpdateStarScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<UpdateStarScenarioResult> {
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    validate_update_star_scenario_input(input.clone()).await?;
    let updated_star = update_in_storage(
        StarEntity::from(input),
        db_pool,
    )
    .await?;
    Ok(UpdateStarScenarioResult::from_entity(updated_star))
}
