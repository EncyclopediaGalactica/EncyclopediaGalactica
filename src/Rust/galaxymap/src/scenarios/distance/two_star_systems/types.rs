#[derive(Debug, Clone)]
pub struct TwoStarSystemsDistanceScenarioInput {
    pub from: String,
    pub to: String,
}

pub struct TwoStarSystemsDistanceScenarioOutput {
    pub distance: f64,
}
