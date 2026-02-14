use sqlx::types::Json;

use crate::star::entities::star_entity::StarEntity;
use crate::star::entities::star_entity_details::StarEntityDetails;

#[derive(Debug, Clone)]
pub struct AddStarScenarioInput {
    pub name: String,
    pub description: String,
}

impl Into<StarEntity> for AddStarScenarioInput {
    fn into(self) -> StarEntity {
        StarEntity::new(0, Json(StarEntityDetails::new(self.name, self.description)))
    }
}
