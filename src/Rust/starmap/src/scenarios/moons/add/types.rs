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
        AddMoonScenarioResult::new(value.id, value.name, value.description)
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
        let entity = MoonEntity::new(2, "Titan".to_string(), "Saturn's moon".to_string());
        let result = AddMoonScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Titan");
        assert_eq!(result.description, "Saturn's moon");
    }
}
