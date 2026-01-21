use pyo3::pyclass;

use crate::scenarios::star_systems::StarSystemEntity;

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddStarSystemScenarioInput {
    pub name: String,
    pub description: String,
    pub x: Option<f64>,
    pub y: Option<f64>,
    pub z: Option<f64>,
}

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddStarSystemScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
    pub x: Option<f64>,
    pub y: Option<f64>,
    pub z: Option<f64>,
}

impl AddStarSystemScenarioResult {
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

impl From<StarSystemEntity> for AddStarSystemScenarioResult {
    fn from(value: StarSystemEntity) -> Self {
        AddStarSystemScenarioResult::new(
            value.id,
            value.name,
            value.description,
            value.x,
            value.y,
            value.z,
        )
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_add_star_system_scenario_result_new() {
        let result = AddStarSystemScenarioResult::new(
            1,
            "Solar System".to_string(),
            "Our star system".to_string(),
            Some(1.0),
            Some(2.0),
            Some(3.0),
        );
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Solar System");
        assert_eq!(result.description, "Our star system");
        assert_eq!(result.x, Some(1.0));
        assert_eq!(result.y, Some(2.0));
        assert_eq!(result.z, Some(3.0));
    }

    #[test]
    fn test_from_star_system_entity() {
        let entity = StarSystemEntity::new(
            2,
            "Alpha Centauri".to_string(),
            "Nearby star system".to_string(),
            Some(4.0),
            Some(5.0),
            Some(6.0),
        );
        let result = AddStarSystemScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Alpha Centauri");
        assert_eq!(result.description, "Nearby star system");
        assert_eq!(result.x, Some(4.0));
        assert_eq!(result.y, Some(5.0));
        assert_eq!(result.z, Some(6.0));
    }
}
