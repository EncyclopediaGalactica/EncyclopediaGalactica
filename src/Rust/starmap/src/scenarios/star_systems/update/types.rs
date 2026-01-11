use serde::{Deserialize, Serialize};

use crate::scenarios::star_systems::StarSystemEntity;

/// Input data structure for updating a star system
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateStarSystemScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
}

/// Result data structure for updating a star system
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateStarSystemScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl UpdateStarSystemScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }

    pub fn from_entity(entity: StarSystemEntity) -> Self {
        Self {
            id: entity.id(),
            name: entity.name().to_string(),
            description: entity.description().to_string(),
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_update_star_system_scenario_result_new() {
        let result = UpdateStarSystemScenarioResult::new(
            1,
            "Solar System".to_string(),
            "A star system".to_string(),
        );
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Solar System");
        assert_eq!(result.description, "A star system");
    }

    #[test]
    fn test_update_star_system_scenario_result_from_entity() {
        let entity = StarSystemEntity::new(
            2,
            "Alpha Centauri".to_string(),
            "Nearby star system".to_string(),
        );
        let result = UpdateStarSystemScenarioResult::from_entity(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Alpha Centauri");
        assert_eq!(result.description, "Nearby star system");
    }
}
