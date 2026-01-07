use pyo3::pyclass;

use crate::logic::starmap::scenarios::planets::PlanetEntity;

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddPlanetScenarioInput {
    pub name: String,
    pub description: String,
}

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddPlanetScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl AddPlanetScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<PlanetEntity> for AddPlanetScenarioResult {
    fn from(value: PlanetEntity) -> Self {
        AddPlanetScenarioResult::new(value.id, value.name, value.description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_add_planet_scenario_result_new() {
        let result =
            AddPlanetScenarioResult::new(1, "Earth".to_string(), "Home planet".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Earth");
        assert_eq!(result.description, "Home planet");
    }

    #[test]
    fn test_from_planet_entity() {
        let entity = PlanetEntity::new(2, "Mars".to_string(), "Red planet".to_string());
        let result = AddPlanetScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Mars");
        assert_eq!(result.description, "Red planet");
    }
}

