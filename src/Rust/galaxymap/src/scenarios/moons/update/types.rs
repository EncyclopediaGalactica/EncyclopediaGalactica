use serde::{Deserialize, Serialize};

use crate::scenarios::moons::MoonEntity;

/// Input data structure for updating a moon
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateMoonScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
}

/// Result data structure for updating a moon
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateMoonScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl UpdateMoonScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }

    pub fn from_entity(entity: MoonEntity) -> Self {
        let name = entity.data()["name"].as_str().unwrap_or("").to_string();
        let description = entity.data()["description"]
            .as_str()
            .unwrap_or("")
            .to_string();
        Self {
            id: entity.id(),
            name,
            description,
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_update_moon_scenario_result_new() {
        let result = UpdateMoonScenarioResult::new(1, "Luna".to_string(), "A moon".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Luna");
        assert_eq!(result.description, "A moon");
    }

    #[test]
    fn test_update_moon_scenario_result_from_entity() {
        let data = serde_json::json!({
            "name": "Titan",
            "description": "Saturn's moon"
        });
        let entity = MoonEntity::new(2, data);
        let result = UpdateMoonScenarioResult::from_entity(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Titan");
        assert_eq!(result.description, "Saturn's moon");
    }
}
