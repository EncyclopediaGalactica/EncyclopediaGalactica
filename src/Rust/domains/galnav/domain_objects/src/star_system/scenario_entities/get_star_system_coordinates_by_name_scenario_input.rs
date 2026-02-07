use serde::Deserialize;
use serde::Serialize;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct StarSystemCoordinatesByNameScenarioInput {
    pub name: String,
}

impl StarSystemCoordinatesByNameScenarioInput {
    pub fn new(name: String) -> Self {
        Self { name }
    }
}
