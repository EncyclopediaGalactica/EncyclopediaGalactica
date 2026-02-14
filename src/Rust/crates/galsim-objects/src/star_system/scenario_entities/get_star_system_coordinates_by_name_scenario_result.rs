use serde::Deserialize;
use serde::Serialize;

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct StarSystemCoordinatesByNameScenarioResult {
    pub x: f64,
    pub y: f64,
    pub z: f64,
}
