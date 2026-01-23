use serde::{Deserialize, Serialize};

use crate::scenarios::moons::MoonEntity;

/// Input data structure for getting all moons (empty for now)
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllMoonsScenarioInput {}

/// Result data structure for a single moon
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllMoonsScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl GetAllMoonsScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<MoonEntity> for GetAllMoonsScenarioResult {
    fn from(entity: MoonEntity) -> Self {
        let name = entity.data["name"].as_str().unwrap_or("").to_string();
        let description = entity.data["description"]
            .as_str()
            .unwrap_or("")
            .to_string();
        GetAllMoonsScenarioResult::new(entity.id, name, description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_get_all_moons_scenario_result_new() {
        let result =
            GetAllMoonsScenarioResult::new(1, "Luna".to_string(), "Earth's moon".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Luna");
        assert_eq!(result.description, "Earth's moon");
    }

    #[test]
    fn test_get_all_moons_scenario_result_from_entity() {
        let data = serde_json::json!({
            "name": "Titan",
            "description": "Saturn's moon"
        });
        let entity = MoonEntity::new(2, data);
        let result = GetAllMoonsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Titan");
        assert_eq!(result.description, "Saturn's moon");
    }
}
