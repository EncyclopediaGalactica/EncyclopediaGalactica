use sqlx::types::Json;

use crate::planet::entities::planet_entity::PlanetEntity;
use crate::planet::entities::planet_entity_details::PlanetEntityDetails;

#[derive(Debug, Clone)]
pub struct AddPlanetScenarioInput {
    pub name: String,
    pub description: String,
}

impl Into<PlanetEntity> for AddPlanetScenarioInput {
    fn into(self) -> PlanetEntity {
        PlanetEntity::new(
            0,
            Json(PlanetEntityDetails::new(self.name, self.description)),
        )
    }
}
