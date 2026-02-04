pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

use self::add::types::AddPlanetScenarioInput;
use self::update::types::UpdatePlanetScenarioInput;
use sqlx::prelude::FromRow;
use sqlx::types::Json;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct PlanetEntityDetails {
    name: String,
    description: String,
}

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct PlanetEntity {
    id: i64,
    details: Json<PlanetEntityDetails>,
}

impl PlanetEntityDetails {
    pub fn new(name: String, description: String) -> Self {
        Self { name, description }
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

impl From<AddPlanetScenarioInput> for PlanetEntity {
    fn from(value: AddPlanetScenarioInput) -> Self {
        let details = PlanetEntityDetails::new(value.name, value.description);
        PlanetEntity::new(0, Json(details))
    }
}

impl From<UpdatePlanetScenarioInput> for PlanetEntity {
    fn from(value: UpdatePlanetScenarioInput) -> Self {
        let details = PlanetEntityDetails::new(value.name, value.description);
        PlanetEntity::new(value.id, Json(details))
    }
}

impl PlanetEntity {
    pub fn new(id: i64, details: Json<PlanetEntityDetails>) -> Self {
        Self { id, details }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn details(&self) -> &Json<PlanetEntityDetails> {
        &self.details
    }

    pub fn set_details(&mut self, details: Json<PlanetEntityDetails>) {
        self.details = details;
    }
}
