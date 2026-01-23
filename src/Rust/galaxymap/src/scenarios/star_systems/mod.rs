use sqlx::prelude::FromRow;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarSystemEntity {
    pub id: i64,
    pub data: serde_json::Value,
}

use self::add::types::AddStarSystemScenarioInput;
use self::update::types::UpdateStarSystemScenarioInput;

pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

impl From<AddStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: AddStarSystemScenarioInput) -> Self {
        let data = serde_json::json!({
            "name": value.name,
            "description": value.description,
            "x": value.x,
            "y": value.y,
            "z": value.z,
        });
        StarSystemEntity::new(0, data)
    }
}

impl From<UpdateStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: UpdateStarSystemScenarioInput) -> Self {
        let data = serde_json::json!({
            "name": value.name,
            "description": value.description,
            "x": value.x,
            "y": value.y,
            "z": value.z,
        });
        StarSystemEntity::new(value.id, data)
    }
}

impl StarSystemEntity {
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
