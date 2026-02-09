use anyhow::ensure;
use gal_nav_domain_objects::planet::scenario_entities::update_planet_scenario_input::UpdatePlanetScenarioInput;

const INVALID_ID_ERROR: &str = "The planet id must be positive.";
const SHORT_NAME_ERROR: &str = "The planet name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str = "The planet description must be longer than 3 characters.";

pub async fn validate_update_planet_scenario_input(
    input: UpdatePlanetScenarioInput,
) -> anyhow::Result<UpdatePlanetScenarioInput> {
    println!("the input: {:?}", input);
    ensure!(input.id > 0, INVALID_ID_ERROR);

    // Validate name
    ensure!(input.name.trim().len() >= 3, SHORT_NAME_ERROR);

    // Validate description
    ensure!(input.description.trim().len() >= 3, SHORT_DESCRIPTION_ERROR);

    Ok(input)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[tokio::test]
    async fn test_validation_valid_input() {
        let input = UpdatePlanetScenarioInput {
            id: 1,
            name: "Earth".to_string(),
            description: "A blue planet".to_string(),
        };
        let result = validate_update_planet_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.id, input.id);
        assert_eq!(result.name, "Earth");
        assert_eq!(result.description, "A blue planet");
    }

    #[tokio::test]
    async fn test_validation_invalid_id_zero() {
        let input = UpdatePlanetScenarioInput {
            id: 0,
            name: "Earth".to_string(),
            description: "A blue planet".to_string(),
        };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), INVALID_ID_ERROR);
    }

    #[tokio::test]
    async fn test_validation_invalid_id_negative() {
        let input = UpdatePlanetScenarioInput {
            id: -1,
            name: "Earth".to_string(),
            description: "A blue planet".to_string(),
        };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), INVALID_ID_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let input = UpdatePlanetScenarioInput {
            id: 1,
            name: "Ab".to_string(),
            description: "A blue planet".to_string(),
        };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let input = UpdatePlanetScenarioInput {
            id: 1,
            name: "Earth".to_string(),
            description: "Ab".to_string(),
        };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_DESCRIPTION_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_name() {
        let input = UpdatePlanetScenarioInput {
            id: 1,
            name: "  Ea  ".to_string(),
            description: "A blue planet".to_string(),
        };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_description() {
        let input = UpdatePlanetScenarioInput {
            id: 1,
            name: "Earth".to_string(),
            description: "  Ab  ".to_string(),
        };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_DESCRIPTION_ERROR);
    }
}
