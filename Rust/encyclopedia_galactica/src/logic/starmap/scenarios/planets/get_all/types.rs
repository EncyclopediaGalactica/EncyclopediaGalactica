use serde::{Deserialize, Serialize};

use crate::logic::starmap::scenarios::planets::PlanetEntity;

/// Input data structure for getting all planets (empty for now)
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllPlanetsScenarioInput {}

/// Result data structure for getting all planets
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllPlanetsScenarioResult {
    pub planets: Vec<PlanetEntity>,
}

impl GetAllPlanetsScenarioResult {
    pub fn new(planets: Vec<PlanetEntity>) -> Self {
        Self { planets }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_get_all_planets_scenario_result_new() {
        let planets = vec![
            PlanetEntity::new(1, "Earth".to_string(), "Home planet".to_string()),
            PlanetEntity::new(2, "Mars".to_string(), "Red planet".to_string()),
        ];
        let result = GetAllPlanetsScenarioResult::new(planets.clone());
        assert_eq!(result.planets.len(), 2);
        assert_eq!(result.planets[0].name, "Earth");
        assert_eq!(result.planets[1].name, "Mars");
    }
}
