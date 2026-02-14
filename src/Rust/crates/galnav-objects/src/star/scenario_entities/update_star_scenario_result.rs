use serde::{Deserialize, Serialize};

use crate::star::entities::star_entity::StarEntity;

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
}

impl From<StarEntity> for UpdateStarScenarioResult {
    fn from(entity: StarEntity) -> Self {
        Self {
            id: entity.id(),
            name: entity.details().name().to_string(),
            description: entity.details().description().to_string(),
        }
    }
}

#[cfg(test)]
mod tests {

    use sqlx::types::Json;

    use crate::star::entities::star_entity::StarEntity;
    use crate::star::entities::star_entity_details::StarEntityDetails;
    use crate::star::scenario_entities::update_star_scenario_result::UpdateStarScenarioResult;

    #[test]
    fn test_update_star_scenario_result_new() {
        let result = UpdateStarScenarioResult::new(1, "Sirius".to_string(), "A star".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Sirius");
        assert_eq!(result.description, "A star");
    }

    #[test]
    fn test_update_star_scenario_result_from_entity() {
        let details = StarEntityDetails::new("Vega".to_string(), "Bright star".to_string());
        let entity = StarEntity::new(2, Json(details));
        let result = UpdateStarScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Vega");
        assert_eq!(result.description, "Bright star");
    }
}
