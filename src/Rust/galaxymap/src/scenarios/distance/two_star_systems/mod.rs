use sqlx::PgPool;

use self::types::TwoStarSystemsDistanceScenarioInput;
use self::types::TwoStarSystemsDistanceScenarioOutput;

pub mod types;

pub async fn calculate_two_star_systems_distance(
    _input: TwoStarSystemsDistanceScenarioInput,
    _db_pool: Option<PgPool>,
    _connection_string: Option<&str>,
) -> anyhow::Result<TwoStarSystemsDistanceScenarioOutput> {
    Ok(TwoStarSystemsDistanceScenarioOutput { distance: 0.0 })
}
