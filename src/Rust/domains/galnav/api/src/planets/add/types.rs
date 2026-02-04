use crate::planets::PlanetEntity;

#[derive(Debug, Clone)]
pub struct AddPlanetScenarioInput {
    pub name: String,
    pub description: String,
}

#[derive(Debug, Clone)]
pub struct AddPlanetScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl AddPlanetScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<PlanetEntity> for AddPlanetScenarioResult {
    fn from(value: PlanetEntity) -> Self {
        let name = value.details.name.to_string();
        let description = value.details.description.to_string();
        AddPlanetScenarioResult::new(value.id, name, description)
    }
}

#[cfg(test)]
mod tests {
    use sqlx::types::Json;

    use crate::planets::PlanetEntityDetails;

    use super::*;

    #[test]
    fn test_add_planet_scenario_result_new() {
        let result =
            AddPlanetScenarioResult::new(1, "Earth".to_string(), "Home planet".to_string());
        assert_eq!(result.id, 1);
        assert_eq!(result.name, "Earth");
        assert_eq!(result.description, "Home planet");
    }

    #[test]
    fn test_from_planet_entity() {
        let details = PlanetEntityDetails::new("Mars".to_string(), "Red planet".to_string());
        let entity = PlanetEntity::new(2, Json(details));
        let result = AddPlanetScenarioResult::from(entity);
        assert_eq!(result.id, 2);
        assert_eq!(result.name, "Mars");
        assert_eq!(result.description, "Red planet");
    }
}
