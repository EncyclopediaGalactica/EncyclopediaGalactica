use sqlx::prelude::FromRow;
use sqlx::types::Json;

use crate::star_system::scenario_entities::add_star_system_scenario_input::AddStarSystemScenarioInput;

use super::star_system_details::StarSystemEntityDetails;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarSystemEntity {
    id: i64,
    details: Json<StarSystemEntityDetails>,
}

impl From<AddStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: AddStarSystemScenarioInput) -> Self {
        let details =
            StarSystemEntityDetails::new(value.name, value.description, value.x, value.y, value.z);
        StarSystemEntity::new(0, Json(details))
    }
}

impl From<UpdateStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: UpdateStarSystemScenarioInput) -> Self {
        let details =
            StarSystemEntityDetails::new(value.name, value.description, value.x, value.y, value.z);
        StarSystemEntity::new(value.id, Json(details))
    }
}

impl StarSystemEntity {
    pub fn new(id: i64, details: Json<StarSystemEntityDetails>) -> Self {
        Self { id, details }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn details(&self) -> &Json<StarSystemEntityDetails> {
        &self.details
    }

    pub fn set_details(&mut self, details: Json<StarSystemEntityDetails>) {
        self.details = details;
    }
}
