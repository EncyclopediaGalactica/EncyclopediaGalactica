use anyhow::ensure;

use super::types::AddStarScenarioInput;

const SHORT_NAME_ERROR: &str = "The star name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str = "The star description must be longer than 3 characters.";

pub async fn validate_add_star_scenario_input(
    input: AddStarScenarioInput,
) -> anyhow::Result<AddStarScenarioInput> {
    println!("the input: {:?}", input);
    ensure!(input.name.trim().len() >= 3, SHORT_NAME_ERROR);
    ensure!(input.description.trim().len() >= 3, SHORT_DESCRIPTION_ERROR);
    Ok(input)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[tokio::test]
    async fn test_validation_valid_input() {
        let input = AddStarScenarioInput {
            name: "Sirius".to_string(),
            description: "Dog star".to_string(),
        };
        let result = validate_add_star_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.name, input.clone().name);
        assert_eq!(result.description, input.clone().description);
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let input = AddStarScenarioInput {
            name: "Hi".to_string(),
            description: "Dog star".to_string(),
        };
        let result = validate_add_star_scenario_input(input).await.unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let input = AddStarScenarioInput {
            name: "Sirius".to_string(),
            description: "Hi".to_string(),
        };
        let result = validate_add_star_scenario_input(input).await.unwrap_err();
        println!("{:?}", result);
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_name() {
        let input = AddStarScenarioInput {
            name: "  A  ".to_string(),
            description: "Dog star".to_string(),
        };
        let result = validate_add_star_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_NAME_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_description() {
        let input = AddStarScenarioInput {
            name: "Sirius".to_string(),
            description: "  Hi                ".to_string(),
        };
        let result = validate_add_star_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }
}
