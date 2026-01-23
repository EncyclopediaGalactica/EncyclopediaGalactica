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
        let name = value.data["name"].as_str().unwrap_or("").to_string();
        let description = value.data["description"].as_str().unwrap_or("").to_string();
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
        let data = serde_json::json!({
            "name": "Vega",
            "description": "Bright star"
        });
        let entity = StarEntity::new(2, data);
        let result = AddStarScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Vega");
        assert_eq!(result.description, "Bright star");
    }
}
