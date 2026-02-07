use serde::{Deserialize, Serialize};

use crate::star_system::entities::star_system::StarSystemEntity;

#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllStarSystemsScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
    pub x: Option<f64>,
    pub y: Option<f64>,
    pub z: Option<f64>,
}

impl GetAllStarSystemsScenarioResult {
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
}

impl From<StarSystemEntity> for GetAllStarSystemsScenarioResult {
    fn from(entity: StarSystemEntity) -> Self {
        let name = entity.details().name().to_string();
        let description = entity.details().description().to_string();
        let x = entity.details().x();
        let y = entity.details().y();
        let z = entity.details().z();
        GetAllStarSystemsScenarioResult::new(entity.id(), name, description, x, y, z)
    }
}

#[cfg(test)]
mod tests {
    use sqlx::types::Json;

    use crate::star_system::entities::star_system_details::StarSystemEntityDetails;

    use super::*;

    #[test]
    fn test_get_all_star_systems_scenario_result_new() {
        let result = GetAllStarSystemsScenarioResult::new(
            1,
            "Solar System".to_string(),
            "Home star system".to_string(),
            Some(13.0),
            Some(14.0),
            Some(15.0),
        );
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Solar System");
        assert_eq!(result.description, "Home star system");
        assert_eq!(result.x, Some(13.0));
        assert_eq!(result.y, Some(14.0));
        assert_eq!(result.z, Some(15.0));
    }

    #[test]
    fn test_get_all_star_systems_scenario_result_from_entity() {
        let data = StarSystemEntityDetails::new(
            "Alpha Centauri".to_string(),
            "Nearby star system".to_string(),
            Some(16.0),
            Some(17.0),
            Some(18.0),
        );
        let entity = StarSystemEntity::new(2, Json(data));
        let result = GetAllStarSystemsScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Alpha Centauri");
        assert_eq!(result.description, "Nearby star system");
        assert_eq!(result.x, Some(16.0));
        assert_eq!(result.y, Some(17.0));
        assert_eq!(result.z, Some(18.0));
    }
}
