use serde::{Deserialize, Serialize};

use crate::scenarios::stars::StarEntity;

/// Input data structure for getting all stars (empty for now)
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllStarsScenarioInput {}

/// Result data structure for a single star
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllStarsScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl GetAllStarsScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<StarEntity> for GetAllStarsScenarioResult {
    fn from(entity: StarEntity) -> Self {
        let name = entity.data["name"].as_str().unwrap_or("").to_string();
        let description = entity.data["description"]
            .as_str()
            .unwrap_or("")
            .to_string();
        GetAllStarsScenarioResult::new(entity.id, name, description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_get_all_stars_scenario_result_new() {
        let result =
            GetAllStarsScenarioResult::new(1, "Sirius".to_string(), "Dog star".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Sirius");
        assert_eq!(result.description, "Dog star");
    }

    #[test]
    fn test_get_all_stars_scenario_result_from_entity() {
        let data = serde_json::json!({
            "name": "Vega",
            "description": "Bright star"
        });
        let entity = StarEntity::new(2, data);
        let result = GetAllStarsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Vega");
        assert_eq!(result.description, "Bright star");
    }
}
