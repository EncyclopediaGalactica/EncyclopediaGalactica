use serde::{Deserialize, Serialize};

use crate::scenarios::star_systems::StarSystemEntity;

/// Input data structure for updating a star system
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateStarSystemScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
    pub x: Option<f64>,
    pub y: Option<f64>,
    pub z: Option<f64>,
}

/// Result data structure for updating a star system
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdateStarSystemScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
    pub x: Option<f64>,
    pub y: Option<f64>,
    pub z: Option<f64>,
}

impl UpdateStarSystemScenarioResult {
    pub fn new(
        id: i64,
        name: String,
        description: String,
        x: Option<f64>,
        y: Option<f64>,
        z: Option<f64>,
    ) -> Self {
        Self {
            id,
            name,
            description,
            x,
            y,
            z,
        }
    }

    pub fn from_entity(entity: StarSystemEntity) -> Self {
        let name = entity.data()["name"].as_str().unwrap_or("").to_string();
        let description = entity.data()["description"]
            .as_str()
            .unwrap_or("")
            .to_string();
        let x = entity.data()["x"].as_f64();
        let y = entity.data()["y"].as_f64();
        let z = entity.data()["z"].as_f64();
        Self {
            id: entity.id(),
            name,
            description,
            x,
            y,
            z,
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
            Some(7.0),
            Some(8.0),
            Some(9.0),
        );
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Solar System");
        assert_eq!(result.description, "A star system");
        assert_eq!(result.x, Some(7.0));
        assert_eq!(result.y, Some(8.0));
        assert_eq!(result.z, Some(9.0));
    }

    #[test]
    fn test_update_star_system_scenario_result_from_entity() {
        let data = serde_json::json!({
            "name": "Alpha Centauri",
            "description": "Nearby star system",
            "x": 10.0,
            "y": 11.0,
            "z": 12.0
        });
        let entity = StarSystemEntity::new(2, data);
        let result = UpdateStarSystemScenarioResult::from_entity(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Alpha Centauri");
        assert_eq!(result.description, "Nearby star system");
        assert_eq!(result.x, Some(10.0));
        assert_eq!(result.y, Some(11.0));
        assert_eq!(result.z, Some(12.0));
    }
}
