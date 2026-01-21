use serde::{Deserialize, Serialize};

use crate::scenarios::planets::PlanetEntity;

/// Input data structure for updating a planet
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdatePlanetScenarioInput {
    pub id: i64,
    pub data: serde_json::Value,
}

/// Result data structure for updating a planet
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdatePlanetScenarioResult {
    pub id: i64,
    pub data: serde_json::Value,
}

impl UpdatePlanetScenarioResult {
    pub fn new(id: i64, data: serde_json::Value) -> Self {
        Self { id, data }
    }

    pub fn from_entity(entity: PlanetEntity) -> Self {
        Self {
            id: entity.id(),
            data: entity.data().clone(),
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_update_planet_scenario_result_new() {
        let data = serde_json::json!({"name": "Earth", "description": "A planet"});
        let result = UpdatePlanetScenarioResult::new(1, data.clone());
        assert_eq!(result.id, 1);
        assert_eq!(result.data, data);
    }

    #[test]
    fn test_update_planet_scenario_result_from_entity() {
        let data = serde_json::json!({"name": "Mars", "description": "Red planet"});
        let entity = PlanetEntity::new(2, data.clone());
        let result = UpdatePlanetScenarioResult::from_entity(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.data, data);
    }
}
