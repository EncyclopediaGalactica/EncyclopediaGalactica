use serde::{Deserialize, Serialize};

use crate::scenarios::planets::PlanetEntity;

/// Input data structure for getting all planets (empty for now)
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllPlanetsScenarioInput {}

/// Result data structure for a single planet
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllPlanetsScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl GetAllPlanetsScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self { id, name, description }
    }
}

impl From<PlanetEntity> for GetAllPlanetsScenarioResult {
    fn from(entity: PlanetEntity) -> Self {
        GetAllPlanetsScenarioResult::new(entity.id, entity.name, entity.description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_get_all_planets_scenario_result_new() {
        let result = GetAllPlanetsScenarioResult::new(1, "Earth".to_string(), "Home planet".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Earth");
        assert_eq!(result.description, "Home planet");
    }

    #[test]
    fn test_get_all_planets_scenario_result_from_entity() {
        let entity = PlanetEntity::new(2, "Mars".to_string(), "Red planet".to_string());
        let result = GetAllPlanetsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Mars");
        assert_eq!(result.description, "Red planet");
    }
}
