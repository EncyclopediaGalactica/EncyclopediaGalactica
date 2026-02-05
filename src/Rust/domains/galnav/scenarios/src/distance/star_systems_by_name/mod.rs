use anyhow::ensure;
use gal_nav_algorithms::distance_by_coordinates::calculate_distance_by_coordinates;
use sqlx::PgPool;

use crate::get_connection;
use crate::star_systems::get_coordinates_by_name::get_star_system_coordinates_by_name;
use crate::star_systems::get_coordinates_by_name::types::StarSystemCoordinatesByNameScenarioInput;

use self::types::TwoStarSystemsDistanceScenarioInput;
use self::types::TwoStarSystemsDistanceScenarioResult;

pub mod types;

pub const FROM_IS_EMPTY: &str = "The star system name cannot be empty";
pub const TO_IS_EMPTY: &str = "The star system name cannot be empty";

/// Calculates the distance between the two given star systems.
/// This calculation is a straight line between the two star systems.
///
/// The formula used for this is the usual Pythagorean theorem in 3D:
/// distance = sqrt(x^2 + y^2 + z^2)
pub async fn distance_of_star_systems_by_name(
    input: TwoStarSystemsDistanceScenarioInput,
    db_pool: Option<PgPool>,
    connection_string: Option<&str>,
) -> anyhow::Result<TwoStarSystemsDistanceScenarioResult> {
    validate_input(&input).await?;
    let db_pool = get_connection(db_pool, connection_string).await?;
    let from_star_system_id = get_star_system_coordinates_by_name(
        StarSystemCoordinatesByNameScenarioInput::new(input.from),
        db_pool.clone(),
    )
    .await?;
    let to_star_system_id = get_star_system_coordinates_by_name(
        StarSystemCoordinatesByNameScenarioInput::new(input.to),
        db_pool.clone(),
    )
    .await?;
    let distance = calculate_distance_by_coordinates(
        &from_star_system_id.x,
        &from_star_system_id.y,
        &from_star_system_id.z,
        &to_star_system_id.x,
        &to_star_system_id.y,
        &to_star_system_id.z,
    )
    .await?;
    Ok(TwoStarSystemsDistanceScenarioResult::new(distance))
}

async fn validate_input(input: &TwoStarSystemsDistanceScenarioInput) -> anyhow::Result<()> {
    ensure!(!input.from.is_empty(), FROM_IS_EMPTY);
    ensure!(!input.to.is_empty(), TO_IS_EMPTY);
    Ok(())
}
