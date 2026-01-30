pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

use self::add::types::AddStarSystemScenarioInput;
use self::update::types::UpdateStarSystemScenarioInput;
use sqlx::prelude::FromRow;
use sqlx::types::Json;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarSystemEntity {
    id: i64,
    details: Json<StarSystemEntityDetails>,
}

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarSystemEntityDetails {
    name: String,
    description: String,
    x: Option<f64>,
    y: Option<f64>,
    z: Option<f64>,
}

impl From<AddStarSystemScenarioInput> for StarSystemEntityDetails {
    fn from(value: AddStarSystemScenarioInput) -> Self {
        StarSystemEntityDetails::new(value.name, value.description, value.x, value.y, value.z)
    }
}
impl From<UpdateStarSystemScenarioInput> for StarSystemEntityDetails {
    fn from(value: UpdateStarSystemScenarioInput) -> Self {
        StarSystemEntityDetails::new(value.name, value.description, value.x, value.y, value.z)
    }
}

impl From<AddStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: AddStarSystemScenarioInput) -> Self {
        let details =
            StarSystemEntityDetails::new(value.name, value.description, value.x, value.y, value.z);
        StarSystemEntity::new(0, Json(details))
    }
}

impl From<UpdateStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: UpdateStarSystemScenarioInput) -> Self {
        let details =
            StarSystemEntityDetails::new(value.name, value.description, value.x, value.y, value.z);
        StarSystemEntity::new(value.id, Json(details))
    }
}

impl StarSystemEntity {
    pub fn new(id: i64, details: Json<StarSystemEntityDetails>) -> Self {
        Self { id, details }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
    }

    pub fn details(&self) -> &Json<StarSystemEntityDetails> {
        &self.details
    }

    pub fn set_details(&mut self, details: Json<StarSystemEntityDetails>) {
        self.details = details;
    }
}
impl StarSystemEntityDetails {
    pub fn new(
        name: String,
        description: String,
        x: Option<f64>,
        y: Option<f64>,
        z: Option<f64>,
    ) -> Self {
        Self {
            name,
            description,
            x,
            y,
            z,
        }
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

    pub fn x(&self) -> Option<f64> {
        self.x
    }

    pub fn set_x(&mut self, x: Option<f64>) {
        self.x = x;
    }

    pub fn set_y(&mut self, y: Option<f64>) {
        self.y = y;
    }

    pub fn y(&self) -> Option<f64> {
        self.y
    }

    pub fn set_z(&mut self, z: Option<f64>) {
        self.z = z;
    }

    pub fn z(&self) -> Option<f64> {
        self.z
    }
}
