use sqlx::prelude::FromRow;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct PlanetEntity {
    pub id: i64,
    pub name: String,
    pub description: String,
}

use self::add::types::AddPlanetScenarioInput;

pub mod add;
pub mod get_all;
pub mod update;

impl From<AddPlanetScenarioInput> for PlanetEntity {
    fn from(value: AddPlanetScenarioInput) -> Self {
        return PlanetEntity::new(0, value.name, value.description);
    }
}

impl PlanetEntity {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn name(&self) -> &str {
        &self.name
    }

    pub fn set_name(&mut self, name: String) {
        self.name = name;
    }

    pub fn description(&self) -> &str {
        &self.description
    }

    pub fn set_description(&mut self, description: String) {
        self.description = description;
    }
}
