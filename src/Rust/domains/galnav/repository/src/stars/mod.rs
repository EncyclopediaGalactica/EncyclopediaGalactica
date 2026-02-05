use sqlx::prelude::FromRow;
use sqlx::types::Json;

pub mod add;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarEntityDetails {
    name: String,
    description: String,
}

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarEntity {
    id: i64,
    details: Json<StarEntityDetails>,
}

impl StarEntityDetails {
    pub fn new(name: String, description: String) -> Self {
        Self { name, description }
    }

    pub fn name(&self) -> &str {
        &self.name
    }

    pub fn set_name(&mut self, name: String) {
        self.name = name;
    }

    pub fn description(&self) -> &str {
        &self.description
    }

    pub fn set_description(&mut self, description: String) {
        self.description = description;
    }
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
