use serde::{Deserialize, Serialize};

/// Input data structure for deleting a planet
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DeletePlanetScenarioInput {
    pub id: i64,
}
