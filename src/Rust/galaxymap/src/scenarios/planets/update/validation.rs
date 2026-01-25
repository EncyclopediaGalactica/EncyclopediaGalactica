use anyhow::ensure;

use super::types::UpdatePlanetScenarioInput;

const INVALID_ID_ERROR: &str = "The planet id must be positive.";
const SHORT_NAME_ERROR: &str = "The planet name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str = "The planet description must be longer than 3 characters.";
const INVALID_DATA_ERROR: &str =
    "The data field must be a valid JSON object with name and description fields.";

pub async fn validate_update_planet_scenario_input(
    input: UpdatePlanetScenarioInput,
) -> anyhow::Result<UpdatePlanetScenarioInput> {
    println!("the input: {:?}", input);
    ensure!(input.id > 0, INVALID_ID_ERROR);

    // Ensure data is an object
    let details_obj = input
        .details
        .as_object()
        .ok_or_else(|| anyhow::anyhow!(INVALID_DATA_ERROR))?;

    // Extract and validate name
    let name = details_obj
        .get("name")
        .and_then(|v| v.as_str())
        .ok_or_else(|| anyhow::anyhow!(INVALID_DATA_ERROR))?;
    ensure!(name.trim().len() >= 3, SHORT_NAME_ERROR);

    // Extract and validate description
    let description = details_obj
        .get("description")
        .and_then(|v| v.as_str())
        .ok_or_else(|| anyhow::anyhow!(INVALID_DATA_ERROR))?;
    ensure!(description.trim().len() >= 3, SHORT_DESCRIPTION_ERROR);

    Ok(input)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[tokio::test]
    async fn test_validation_valid_input() {
        let details = serde_json::json!({"name": "Earth", "description": "A blue planet"});
        let input = UpdatePlanetScenarioInput {
            id: 1,
            details: details.clone(),
        };
        let result = validate_update_planet_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.id, input.id);
        assert_eq!(result.details, details);
    }

    #[tokio::test]
    async fn test_validation_invalid_id_zero() {
        let details = serde_json::json!({"name": "Earth", "description": "A blue planet"});
        let input = UpdatePlanetScenarioInput { id: 0, details };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), INVALID_ID_ERROR);
    }

    #[tokio::test]
    async fn test_validation_invalid_id_negative() {
        let details = serde_json::json!({"name": "Earth", "description": "A blue planet"});
        let input = UpdatePlanetScenarioInput { id: -1, details };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), INVALID_ID_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let details = serde_json::json!({"name": "Ab", "description": "A blue planet"});
        let input = UpdatePlanetScenarioInput { id: 1, details };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let details = serde_json::json!({"name": "Earth", "description": "Ab"});
        let input = UpdatePlanetScenarioInput { id: 1, details };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_DESCRIPTION_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_name() {
        let details = serde_json::json!({"name": "  Ea  ", "description": "A blue planet"});
        let input = UpdatePlanetScenarioInput { id: 1, details };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_description() {
        let details = serde_json::json!({"name": "Earth", "description": "  Ab  "});
        let input = UpdatePlanetScenarioInput { id: 1, details };
        let result = validate_update_planet_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_DESCRIPTION_ERROR);
    }
}
