use serde::{Deserialize, Serialize};

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DeleteStarScenarioInput {
    pub id: i64,
}
