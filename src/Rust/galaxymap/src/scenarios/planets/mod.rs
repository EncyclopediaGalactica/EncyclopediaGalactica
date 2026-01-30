pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

use self::add::types::AddPlanetScenarioInput;
use sqlx::prelude::FromRow;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct PlanetEntity {
    pub id: i64,
    pub details: serde_json::Value,
}

impl From<AddPlanetScenarioInput> for PlanetEntity {
    fn from(value: AddPlanetScenarioInput) -> Self {
        return PlanetEntity::new(0, value.details);
    }
}

impl PlanetEntity {
    pub fn new(id: i64, details: serde_json::Value) -> Self {
        Self { id, details }
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
