use pyo3::pyclass;

use crate::scenarios::moons::MoonEntity;

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddMoonScenarioInput {
    pub name: String,
    pub description: String,
}

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddMoonScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl AddMoonScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<MoonEntity> for AddMoonScenarioResult {
    fn from(value: MoonEntity) -> Self {
        let name = value.data["name"].as_str().unwrap_or("").to_string();
        let description = value.data["description"].as_str().unwrap_or("").to_string();
        AddMoonScenarioResult::new(value.id, name, description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_add_moon_scenario_result_new() {
        let result = AddMoonScenarioResult::new(1, "Luna".to_string(), "Earth's moon".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Luna");
        assert_eq!(result.description, "Earth's moon");
    }

    #[test]
    fn test_from_moon_entity() {
        let data = serde_json::json!({
            "name": "Titan",
            "description": "Saturn's moon"
        });
        let entity = MoonEntity::new(2, data);
        let result = AddMoonScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Titan");
        assert_eq!(result.description, "Saturn's moon");
    }
}
