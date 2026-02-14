use gal_nav_domain_objects::star_system::scenario_entities::get_star_system_coordinates_by_name_scenario_input::StarSystemCoordinatesByNameScenarioInput;
use gal_nav_domain_objects::star_system::scenario_entities::get_star_system_coordinates_by_name_scenario_result::StarSystemCoordinatesByNameScenarioResult;

use super::validation::validate_input;

pub async fn get_star_system_coordinates_by_name(
    input: StarSystemCoordinatesByNameScenarioInput,
    _pool: sqlx::PgPool,
) -> anyhow::Result<StarSystemCoordinatesByNameScenarioResult> {
    validate_input(&input).await?;
    Ok(StarSystemCoordinatesByNameScenarioResult {
        x: 0.0,
        y: 0.0,
        z: 0.0,
    })
}
