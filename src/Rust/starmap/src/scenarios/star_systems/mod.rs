use sqlx::prelude::FromRow;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarSystemEntity {
    pub id: i64,
    pub name: String,
    pub description: String,
}

use self::add::types::AddStarSystemScenarioInput;
use self::update::types::UpdateStarSystemScenarioInput;

pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

impl From<AddStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: AddStarSystemScenarioInput) -> Self {
        return StarSystemEntity::new(0, value.name, value.description);
    }
}

impl From<UpdateStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: UpdateStarSystemScenarioInput) -> Self {
        return StarSystemEntity::new(value.id, value.name, value.description);
    }
}

impl StarSystemEntity {
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
