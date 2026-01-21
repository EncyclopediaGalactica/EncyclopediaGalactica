use serde::{Deserialize, Serialize};

/// Input data structure for deleting a moon
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DeleteMoonScenarioInput {
    pub id: i64,
}
