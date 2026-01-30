use self::types::StarSystemCoordinatesByNameScenarioInput;
use self::types::StarSystemCoordinatesByNameScenarioResult;
use self::validation::validate_input;

pub mod storage;
pub mod types;
pub mod validation;

pub async fn get_star_system_coordinates_by_name(
    input: StarSystemCoordinatesByNameScenarioInput,
    pool: sqlx::PgPool,
) -> anyhow::Result<StarSystemCoordinatesByNameScenarioResult> {
    validate_input(&input).await?;
    Ok(StarSystemCoordinatesByNameScenarioResult {
        x: 0.0,
        y: 0.0,
        z: 0.0,
    })
}
