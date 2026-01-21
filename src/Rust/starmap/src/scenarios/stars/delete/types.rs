use serde::{Deserialize, Serialize};

/// Input data structure for deleting a star
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DeleteStarScenarioInput {
    pub id: i64,
}
