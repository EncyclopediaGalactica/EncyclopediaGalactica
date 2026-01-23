use sqlx::prelude::FromRow;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarEntity {
    pub id: i64,
    pub data: serde_json::Value,
}

use self::add::types::AddStarScenarioInput;
use self::update::types::UpdateStarScenarioInput;

pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

impl From<AddStarScenarioInput> for StarEntity {
    fn from(value: AddStarScenarioInput) -> Self {
        let data = serde_json::json!({
            "name": value.name,
            "description": value.description,
        });
        StarEntity::new(0, data)
    }
}

impl From<UpdateStarScenarioInput> for StarEntity {
    fn from(value: UpdateStarScenarioInput) -> Self {
        let data = serde_json::json!({
            "name": value.name,
            "description": value.description,
        });
        StarEntity::new(value.id, data)
    }
}

impl StarEntity {
    pub fn new(id: i64, data: serde_json::Value) -> Self {
        Self { id, data }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn data(&self) -> &serde_json::Value {
        &self.data
    }

    pub fn set_data(&mut self, data: serde_json::Value) {
        self.data = data;
    }
}
