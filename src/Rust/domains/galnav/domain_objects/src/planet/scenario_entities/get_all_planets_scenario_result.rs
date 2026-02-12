use serde::{Deserialize, Serialize};

use crate::planet::entities::planet_entity::PlanetEntity;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllPlanetsScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl GetAllPlanetsScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<PlanetEntity> for GetAllPlanetsScenarioResult {
    fn from(entity: PlanetEntity) -> Self {
        GetAllPlanetsScenarioResult::new(
            entity.id(),
            entity.details().name().to_string(),
            entity.details().description().to_string(),
        )
    }
}

#[cfg(test)]
mod tests {
    use sqlx::types::Json;

    use crate::planet::entities::planet_entity_details::PlanetEntityDetails;

    use super::*;

    #[test]
    fn test_get_all_planets_scenario_result_from_entity() {
        let details = PlanetEntityDetails::new("Mars".to_string(), "Red planet".to_string());
        let entity = PlanetEntity::new(2, Json(details));
        let result = GetAllPlanetsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Mars");
        assert_eq!(result.description, "Red planet");
    }
}
