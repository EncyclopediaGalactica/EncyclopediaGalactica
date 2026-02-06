#[derive(Debug, Clone)]
pub struct AddStarSystemScenarioInput {
    pub name: String,
    pub description: String,
    pub x: Option<f64>,
    pub y: Option<f64>,
    pub z: Option<f64>,
}
