use serde::{Deserialize, Serialize};

use crate::scenarios::stars::StarEntity;

/// Input data structure for updating a star
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateStarScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
}

/// Result data structure for updating a star
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateStarScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl UpdateStarScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }

    pub fn from_entity(entity: StarEntity) -> Self {
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
    fn test_update_star_scenario_result_new() {
        let result =
            UpdateStarScenarioResult::new(1, "Sirius".to_string(), "A star".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Sirius");
        assert_eq!(result.description, "A star");
    }

    #[test]
    fn test_update_star_scenario_result_from_entity() {
        let entity = StarEntity::new(2, "Vega".to_string(), "Bright star".to_string());
        let result = UpdateStarScenarioResult::from_entity(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Vega");
        assert_eq!(result.description, "Bright star");
    }
}