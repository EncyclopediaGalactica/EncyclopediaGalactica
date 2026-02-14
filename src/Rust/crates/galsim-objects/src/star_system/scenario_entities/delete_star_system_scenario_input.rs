use serde::{Deserialize, Serialize};

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DeleteStarSystemScenarioInput {
    pub id: i64,
}
