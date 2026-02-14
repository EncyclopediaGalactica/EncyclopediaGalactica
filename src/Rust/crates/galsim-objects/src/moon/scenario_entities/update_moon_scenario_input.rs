use serde::{Deserialize, Serialize};
use sqlx::types::Json;

use crate::moon::entities::moon_entity::MoonEntity;
use crate::moon::entities::moon_entity_details::MoonEntityDetails;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateMoonScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl Into<MoonEntity> for UpdateMoonScenarioInput {
    fn into(self) -> MoonEntity {
        let data = MoonEntityDetails::new(self.name, self.description);
        MoonEntity::new(self.id, Json(data))
    }
}
