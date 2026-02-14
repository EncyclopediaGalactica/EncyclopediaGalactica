use sqlx::prelude::FromRow;
use sqlx::types::Json;

use super::moon_entity_details::MoonEntityDetails;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct MoonEntity {
    id: i64,
    details: Json<MoonEntityDetails>,
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
