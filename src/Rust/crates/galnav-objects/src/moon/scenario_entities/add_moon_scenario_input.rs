use sqlx::types::Json;

use crate::moon::entities::moon_entity::MoonEntity;
use crate::moon::entities::moon_entity_details::MoonEntityDetails;

#[derive(Debug, Clone)]
pub struct AddMoonScenarioInput {
    pub name: String,
    pub description: String,
}

impl Into<MoonEntity> for AddMoonScenarioInput {
    fn into(self) -> MoonEntity {
        let data = MoonEntityDetails::new(self.name, self.description);
        MoonEntity::new(0, Json(data))
    }
}
