use serde::Deserialize;
use serde::Serialize;

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct TwoStarSystemsDistanceScenarioInput {
    pub from: String,
    pub to: String,
}

pub struct TwoStarSystemsDistanceScenarioResult {
    pub distance: f64,
}

impl TwoStarSystemsDistanceScenarioResult {
    pub fn new(distance: f64) -> Self {
        Self { distance }
    }
}
