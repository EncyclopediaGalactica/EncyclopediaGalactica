use serde::{Deserialize, Serialize};

use crate::scenarios::planets::PlanetEntity;

/// Input data structure for getting all planets (empty for now)
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllPlanetsScenarioInput {}

/// Result data structure for a single planet
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllPlanetsScenarioResult {
    pub id: i64,
    pub data: serde_json::Value,
}

impl GetAllPlanetsScenarioResult {
    pub fn new(id: i64, data: serde_json::Value) -> Self {
        Self { id, data }
    }
}

impl From<PlanetEntity> for GetAllPlanetsScenarioResult {
    fn from(entity: PlanetEntity) -> Self {
        GetAllPlanetsScenarioResult::new(entity.id, entity.data)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_get_all_planets_scenario_result_from_entity() {
        let data = serde_json::json!({"name": "Mars", "description": "Red planet"});
        let entity = PlanetEntity::new(2, data.clone());
        let result = GetAllPlanetsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.data, data);
    }
}
