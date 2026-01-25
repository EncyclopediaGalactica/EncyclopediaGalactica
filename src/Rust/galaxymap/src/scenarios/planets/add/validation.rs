use anyhow::ensure;

use super::types::AddPlanetScenarioInput;

const SHORT_NAME_ERROR: &str = "The planet name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str = "The planet description must be longer than 3 characters.";
const INVALID_DATA_ERROR: &str =
    "The data field must be a valid JSON object with name and description fields.";

pub async fn validate_add_planet_scenario_input(
    input: AddPlanetScenarioInput,
) -> anyhow::Result<AddPlanetScenarioInput> {
    println!("the input: {:?}", input);

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
        let details = serde_json::json!({"name": "Earth", "description": "Home planet"});
        let input = AddPlanetScenarioInput {
            details: details.clone(),
        };
        let result = validate_add_planet_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.details, details);
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let details = serde_json::json!({"name": "Hi", "description": "Home planet"});
        let input = AddPlanetScenarioInput { details };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let details = serde_json::json!({"name": "Earth", "description": "Hi"});
        let input = AddPlanetScenarioInput { details };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        println!("{:?}", result);
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_name() {
        let details = serde_json::json!({"name": "  A  ", "description": "Home planet"});
        let input = AddPlanetScenarioInput { details };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_NAME_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_description() {
        let details = serde_json::json!({"name": "Earth", "description": "  Hi                "});
        let input = AddPlanetScenarioInput { details };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }

    #[tokio::test]
    async fn test_validation_invalid_data_not_object() {
        let details = serde_json::json!("not an object");
        let input = AddPlanetScenarioInput { details };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert_eq!(result.to_string(), INVALID_DATA_ERROR);
    }

    #[tokio::test]
    async fn test_validation_invalid_data_missing_name() {
        let details = serde_json::json!({"description": "Home planet"});
        let input = AddPlanetScenarioInput { details };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert_eq!(result.to_string(), INVALID_DATA_ERROR);
    }

    #[tokio::test]
    async fn test_validation_invalid_data_missing_description() {
        let details = serde_json::json!({"name": "Earth"});
        let input = AddPlanetScenarioInput { details };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert_eq!(result.to_string(), INVALID_DATA_ERROR);
    }
}
