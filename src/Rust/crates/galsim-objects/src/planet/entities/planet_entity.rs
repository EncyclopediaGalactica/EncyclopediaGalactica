use sqlx::prelude::FromRow;
use sqlx::types::Json;

use super::planet_entity_details::PlanetEntityDetails;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct PlanetEntity {
    id: i64,
    details: Json<PlanetEntityDetails>,
}

impl PlanetEntity {
    pub fn new(id: i64, details: Json<PlanetEntityDetails>) -> Self {
        Self { id, details }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn details(&self) -> &Json<PlanetEntityDetails> {
        &self.details
    }

    pub fn set_details(&mut self, details: Json<PlanetEntityDetails>) {
        self.details = details;
    }
}
