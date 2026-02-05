use serde::Deserialize;
use serde::Serialize;
use sqlx::types::Json;

use crate::star::entities::star_entity::StarEntity;
use crate::star::entities::star_entity_details::StarEntityDetails;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateStarScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
}
impl Into<StarEntity> for UpdateStarScenarioInput {
    fn into(self) -> StarEntity {
        StarEntity::new(0, Json(StarEntityDetails::new(self.name, self.description)))
    }
}
