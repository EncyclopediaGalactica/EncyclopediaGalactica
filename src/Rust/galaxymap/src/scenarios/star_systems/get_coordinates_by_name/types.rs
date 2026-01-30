#[derive(Debug, Clone)]
pub struct StarSystemCoordinatesByNameScenarioInput {
    pub name: String,
}

impl StarSystemCoordinatesByNameScenarioInput {
    pub fn new(name: String) -> Self {
        Self { name }
    }
}

#[derive(Debug, Clone)]
pub struct StarSystemCoordinatesByNameScenarioResult {
    pub x: f64,
    pub y: f64,
    pub z: f64,
}
