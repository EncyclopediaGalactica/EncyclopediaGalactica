use pyo3::pyclass;

use crate::logic::starmap::scenarios::star_systems::StarSystemEntity;

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddStarSystemScenarioInput {
    pub name: String,
    pub description: String,
}

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddStarSystemScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl AddStarSystemScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<StarSystemEntity> for AddStarSystemScenarioResult {
    fn from(value: StarSystemEntity) -> Self {
        AddStarSystemScenarioResult::new(value.id, value.name, value.description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_add_star_system_scenario_result_new() {
        let result = AddStarSystemScenarioResult::new(
            1,
            "Solar System".to_string(),
            "Our star system".to_string(),
        );
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Solar System");
        assert_eq!(result.description, "Our star system");
    }

    #[test]
    fn test_from_star_system_entity() {
        let entity = StarSystemEntity::new(
            2,
            "Alpha Centauri".to_string(),
            "Nearby star system".to_string(),
        );
        let result = AddStarSystemScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Alpha Centauri");
        assert_eq!(result.description, "Nearby star system");
    }
}
