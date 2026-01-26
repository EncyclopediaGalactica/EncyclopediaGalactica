pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

use self::add::types::AddMoonScenarioInput;
use self::update::types::UpdateMoonScenarioInput;
use sqlx::prelude::FromRow;
use sqlx::types::Json;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct MoonEntity {
    id: i64,
    details: Json<MoonEntityDetails>,
}

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct MoonEntityDetails {
    name: String,
    description: String,
}

impl From<AddMoonScenarioInput> for MoonEntity {
    fn from(value: AddMoonScenarioInput) -> Self {
        let data = MoonEntityDetails::new(value.name, value.description);
        MoonEntity::new(0, Json(data))
    }
}

impl From<UpdateMoonScenarioInput> for MoonEntity {
    fn from(value: UpdateMoonScenarioInput) -> Self {
        let data = MoonEntityDetails::new(value.name, value.description);
        MoonEntity::new(value.id, Json(data))
    }
}

impl MoonEntityDetails {
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

impl MoonEntity {
    pub fn new(id: i64, details: Json<MoonEntityDetails>) -> Self {
        Self { id, details }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn details(&self) -> &Json<MoonEntityDetails> {
        &self.details
    }

    pub fn set_details(&mut self, details: Json<MoonEntityDetails>) {
        self.details = details;
    }
}
