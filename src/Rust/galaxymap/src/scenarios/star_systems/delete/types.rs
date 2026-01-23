use serde::{Deserialize, Serialize};

/// Input data structure for deleting a star system
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DeleteStarSystemScenarioInput {
    pub id: i64,
}
