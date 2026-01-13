use serde::{Deserialize, Serialize};

use crate::scenarios::star_systems::StarSystemEntity;

/// Input data structure for getting all star systems (empty for now)
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllStarSystemsScenarioInput {}

/// Result data structure for a single star system
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllStarSystemsScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl GetAllStarSystemsScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self { id, name, description }
    }
}

impl From<StarSystemEntity> for GetAllStarSystemsScenarioResult {
    fn from(entity: StarSystemEntity) -> Self {
        GetAllStarSystemsScenarioResult::new(entity.id, entity.name, entity.description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_get_all_star_systems_scenario_result_new() {
        let result = GetAllStarSystemsScenarioResult::new(1, "Solar System".to_string(), "Home star system".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Solar System");
        assert_eq!(result.description, "Home star system");
    }

    #[test]
    fn test_get_all_star_systems_scenario_result_from_entity() {
        let entity = StarSystemEntity::new(
            2,
            "Alpha Centauri".to_string(),
            "Nearby star system".to_string(),
        );
        let result = GetAllStarSystemsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Alpha Centauri");
        assert_eq!(result.description, "Nearby star system");
    }
}
