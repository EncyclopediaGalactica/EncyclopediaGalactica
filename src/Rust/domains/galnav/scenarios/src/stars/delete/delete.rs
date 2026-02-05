use gal_nav_domain_objects::star::scenario_entities::delete_star_scenario_input::DeleteStarScenarioInput;
use gal_nav_repository::stars::delete_by_id::delete_star_by_id;
use sqlx::PgPool;

use crate::get_connection;

use super::validation::validate_delete_star_scenario_input;

/// Deletes a star from the starmap
///
/// This scenario can be called from the Python API too and that will provide
/// a connection string instead of the `PgPool`.
pub async fn delete_star_scenario(
    input: DeleteStarScenarioInput,
    pg_pool: Option<PgPool>,
    db_connection_string: Option<&str>,
) -> anyhow::Result<()> {
    validate_delete_star_scenario_input(&input)?;
    let db_pool = get_connection(pg_pool, db_connection_string).await?;
    delete_star_by_id(input.id, db_pool).await?;
    Ok(())
}
