use sqlx::prelude::FromRow;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct MoonEntity {
    pub id: i64,
    pub details: serde_json::Value,
}

use self::add::types::AddMoonScenarioInput;
use self::update::types::UpdateMoonScenarioInput;

pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

impl From<AddMoonScenarioInput> for MoonEntity {
    fn from(value: AddMoonScenarioInput) -> Self {
        let data = serde_json::json!({
            "name": value.name,
            "description": value.description,
        });
        MoonEntity::new(0, data)
    }
}

impl From<UpdateMoonScenarioInput> for MoonEntity {
    fn from(value: UpdateMoonScenarioInput) -> Self {
        let data = serde_json::json!({
            "name": value.name,
            "description": value.description,
        });
        MoonEntity::new(value.id, data)
    }
}

impl MoonEntity {
    pub fn new(id: i64, data: serde_json::Value) -> Self {
        Self { id, details: data }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn data(&self) -> &serde_json::Value {
        &self.details
    }

    pub fn set_data(&mut self, data: serde_json::Value) {
        self.details = data;
    }
}
