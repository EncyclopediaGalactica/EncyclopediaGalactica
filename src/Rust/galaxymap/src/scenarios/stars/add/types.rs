use pyo3::pyclass;

use crate::scenarios::stars::StarEntity;

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddStarScenarioInput {
    pub name: String,
    pub description: String,
}

#[derive(Debug, Clone)]
#[pyclass]
pub struct AddStarScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl AddStarScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<StarEntity> for AddStarScenarioResult {
    fn from(value: StarEntity) -> Self {
        let name = value.details.name.to_string();
        let description = value.details.description.to_string();
        AddStarScenarioResult::new(value.id, name, description)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_add_star_scenario_result_new() {
        let result = AddStarScenarioResult::new(1, "Sirius".to_string(), "Dog star".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Sirius");
        assert_eq!(result.description, "Dog star");
    }

    #[test]
    fn test_from_star_entity() {
        use sqlx::types::Json;

        use crate::scenarios::stars::StarEntityDetails;

        let details = StarEntityDetails::new("Vega".to_string(), "Bright star".to_string());
        let entity = StarEntity::new(2, Json(details));
        let result = AddStarScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Vega");
        assert_eq!(result.description, "Bright star");
    }
}
