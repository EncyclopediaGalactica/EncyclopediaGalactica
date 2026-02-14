use serde::Deserialize;
use serde::Serialize;

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct TwoStarSystemsDistanceScenarioInput {
    pub from: String,
    pub to: String,
}
