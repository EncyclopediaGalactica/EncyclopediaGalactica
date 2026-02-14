use sqlx::prelude::FromRow;
use sqlx::types::Json;

use super::star_entity_details::StarEntityDetails;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarEntity {
    id: i64,
    details: Json<StarEntityDetails>,
}
impl StarEntity {
    pub fn new(id: i64, details: Json<StarEntityDetails>) -> Self {
        Self { id, details }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn details(&self) -> &Json<StarEntityDetails> {
        &self.details
    }

    pub fn set_details(&mut self, details: Json<StarEntityDetails>) {
        self.details = details;
    }
}
