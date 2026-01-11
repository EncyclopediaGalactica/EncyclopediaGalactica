use serde::{Deserialize, Serialize};

use crate::scenarios::star_systems::StarSystemEntity;

/// Input data structure for getting all star systems (empty for now)
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllStarSystemsScenarioInput {}

/// Result data structure for getting all star systems
#[derive(Debug, Deserialize, Serialize, Clone)]
pub struct GetAllStarSystemsScenarioResult {
    pub star_systems: Vec<StarSystemEntity>,
}

impl GetAllStarSystemsScenarioResult {
    pub fn new(star_systems: Vec<StarSystemEntity>) -> Self {
        Self { star_systems }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_get_all_star_systems_scenario_result_new() {
        let star_systems = vec![
            StarSystemEntity::new(
                1,
                "Solar System".to_string(),
                "Home star system".to_string(),
            ),
            StarSystemEntity::new(
                2,
                "Alpha Centauri".to_string(),
                "Nearby star system".to_string(),
            ),
        ];
        let result = GetAllStarSystemsScenarioResult::new(star_systems.clone());
        assert_eq!(result.star_systems.len(), 2);
        assert_eq!(result.star_systems[0].name, "Solar System");
        assert_eq!(result.star_systems[1].name, "Alpha Centauri");
    }
}
