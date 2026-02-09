use serde::{Deserialize, Serialize};
use sqlx::types::Json;

use crate::planet::entities::planet_entity::PlanetEntity;
use crate::planet::entities::planet_entity_details::PlanetEntityDetails;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdatePlanetScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl Into<PlanetEntity> for UpdatePlanetScenarioInput {
    fn into(self) -> PlanetEntity {
        PlanetEntity::new(
            self.id,
            Json(PlanetEntityDetails::new(self.name, self.description)),
        )
    }
}
