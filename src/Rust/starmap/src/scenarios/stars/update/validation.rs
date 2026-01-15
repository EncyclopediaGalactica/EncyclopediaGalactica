use anyhow::ensure;

use super::types::UpdateStarScenarioInput;

const SHORT_NAME_ERROR: &str = "The star name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str = "The star description must be longer than 3 characters.";
const INVALID_ID_ERROR: &str = "The star id must be greater than 0.";

pub async fn validate_update_star_scenario_input(
    input: UpdateStarScenarioInput,
) -> anyhow::Result<UpdateStarScenarioInput> {
    ensure!(input.id > 0, INVALID_ID_ERROR);
    ensure!(input.name.trim().len() >= 3, SHORT_NAME_ERROR);
    ensure!(input.description.trim().len() >= 3, SHORT_DESCRIPTION_ERROR);
    Ok(input)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[tokio::test]
    async fn test_validation_valid_input() {
        let input = UpdateStarScenarioInput {
            id: 1,
            name: "Sirius".to_string(),
            description: "Dog star".to_string(),
        };
        let result = validate_update_star_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.id, input.id);
        assert_eq!(result.name, input.name);
        assert_eq!(result.description, input.description);
    }

    #[tokio::test]
    async fn test_validation_invalid_id() {
        let input = UpdateStarScenarioInput {
            id: 0,
            name: "Sirius".to_string(),
            description: "Dog star".to_string(),
        };
        let result = validate_update_star_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(INVALID_ID_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let input = UpdateStarScenarioInput {
            id: 1,
            name: "Hi".to_string(),
            description: "Dog star".to_string(),
        };
        let result = validate_update_star_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_NAME_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let input = UpdateStarScenarioInput {
            id: 1,
            name: "Sirius".to_string(),
            description: "Hi".to_string(),
        };
        let result = validate_update_star_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }
}