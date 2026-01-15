use sqlx::prelude::FromRow;

#[derive(Debug, Clone, FromRow, serde::Deserialize, serde::Serialize)]
pub struct StarSystemEntity {
    pub id: i64,
    pub name: String,
    pub description: String,
    pub x: Option<f64>,
    pub y: Option<f64>,
    pub z: Option<f64>,
}

use self::add::types::AddStarSystemScenarioInput;
use self::update::types::UpdateStarSystemScenarioInput;

pub mod add;
pub mod delete;
pub mod get_all;
pub mod update;

impl From<AddStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: AddStarSystemScenarioInput) -> Self {
        StarSystemEntity::new(0, value.name, value.description, value.x, value.y, value.z)
    }
}

impl From<UpdateStarSystemScenarioInput> for StarSystemEntity {
    fn from(value: UpdateStarSystemScenarioInput) -> Self {
        StarSystemEntity::new(value.id, value.name, value.description, value.x, value.y, value.z)
    }
}

impl StarSystemEntity {
    pub fn new(id: i64, name: String, description: String, x: Option<f64>, y: Option<f64>, z: Option<f64>) -> Self {
        Self {
            id,
            name,
            description,
            x,
            y,
            z,
        }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn set_id(&mut self, id: i64) {
        self.id = id;
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

    pub fn y(&self) -> Option<f64> {
        self.y
    }

    pub fn set_y(&mut self, y: Option<f64>) {
        self.y = y;
    }

    pub fn z(&self) -> Option<f64> {
        self.z
    }

    pub fn set_z(&mut self, z: Option<f64>) {
        self.z = z;
    }
}
