use serde::{Deserialize, Serialize};

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct DeleteMoonScenarioInput {
    pub id: i64,
}
