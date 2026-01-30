use serde::{Deserialize, Serialize};

use crate::scenarios::planets::PlanetEntity;

/// Input data structure for updating a planet
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdatePlanetScenarioInput {
    pub id: i64,
    pub name: String,
    pub description: String,
}

/// Result data structure for updating a planet
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct UpdatePlanetScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl UpdatePlanetScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }

    pub fn from_entity(entity: PlanetEntity) -> Self {
        Self {
            id: entity.id(),
            name: entity.details().name.to_string(),
            description: entity.details().description.to_string(),
        }
    }
}

#[cfg(test)]
mod tests {
    use sqlx::types::Json;

    use crate::scenarios::planets::PlanetEntityDetails;

    use super::*;

    #[test]
    fn test_update_planet_scenario_result_new() {
        let result =
            UpdatePlanetScenarioResult::new(1, "Earth".to_string(), "A planet".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Earth");
        assert_eq!(result.description, "A planet");
    }

    #[test]
    fn test_update_planet_scenario_result_from_entity() {
        let details = PlanetEntityDetails::new("Mars".to_string(), "Red planet".to_string());
        let entity = PlanetEntity::new(2, Json(details));
        let result = UpdatePlanetScenarioResult::from_entity(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Mars");
        assert_eq!(result.description, "Red planet");
    }
}
