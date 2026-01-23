use pyo3::pyclass;

use crate::scenarios::planets::PlanetEntity;

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddPlanetScenarioInput {
    pub data: serde_json::Value,
}

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddPlanetScenarioResult {
    pub id: i64,
    pub data: serde_json::Value,
}

impl AddPlanetScenarioResult {
    pub fn new(id: i64, data: serde_json::Value) -> Self {
        Self { id, data }
    }
}

impl From<PlanetEntity> for AddPlanetScenarioResult {
    fn from(value: PlanetEntity) -> Self {
        AddPlanetScenarioResult::new(value.id, value.data)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_add_planet_scenario_result_new() {
        let data = serde_json::json!({"name": "Earth", "description": "Home planet"});
        let result = AddPlanetScenarioResult::new(1, data.clone());
        assert_eq!(result.id, 1);
        assert_eq!(result.data, data);
    }

    #[test]
    fn test_from_planet_entity() {
        let data = serde_json::json!({"name": "Mars", "description": "Red planet"});
        let entity = PlanetEntity::new(2, data.clone());
        let result = AddPlanetScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.data, data);
    }
}
