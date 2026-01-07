use crate::logic::starmap::get_connection;
use crate::logic::starmap::scenarios::planets::PlanetEntity;

use super::storage::add_to_storage;
use super::types::{AddPlanetScenarioInput, AddPlanetScenarioResult};
use super::validation::validate_add_planet_scenario_input;

pub async fn add_planet_scenario(
    input: AddPlanetScenarioInput,
    db_connection_string: &str,
) -> anyhow::Result<AddPlanetScenarioResult> {
    validate_add_planet_scenario_input(input.clone()).await?;
    let db_connection = get_connection(db_connection_string).await?;
    let recorded_planet = add_to_storage(PlanetEntity::from(input), db_connection).await?;
    Ok(AddPlanetScenarioResult::from(recorded_planet))
}

#[cfg(test)]
mod tests {
    use super::*;

    #[tokio::test]
    async fn test_add_planet_scenario_invalid_input() {
        let input = AddPlanetScenarioInput {
            name: "Hi".to_string(),
            description: "Home planet".to_string(),
        };
        let result = add_planet_scenario(input, "dummy").await;
        assert!(result.is_err());
    }
}
