use serde::{Deserialize, Serialize};

use crate::moons::MoonEntity;

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
        let name = entity.details.name.to_string();
        let description = entity.details.description.to_string();
        GetAllMoonsScenarioResult::new(entity.id, name, description)
    }
}

#[cfg(test)]
mod tests {
    use sqlx::types::Json;

    use crate::moons::MoonEntityDetails;

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
        let data = MoonEntityDetails::new("Titan".to_string(), "Saturn's moon".to_string());
        let entity = MoonEntity::new(2, Json(data));
        let result = GetAllMoonsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Titan");
        assert_eq!(result.description, "Saturn's moon");
    }
}
