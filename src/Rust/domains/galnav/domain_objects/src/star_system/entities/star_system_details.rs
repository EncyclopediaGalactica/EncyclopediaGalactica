use sqlx::prelude::FromRow;

use crate::star_system::scenario_entities::add_star_system_scenario_input::AddStarSystemScenarioInput;
use crate::star_system::scenario_entities::update_star_system_scenario_input::UpdateStarSystemScenarioInput;

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
