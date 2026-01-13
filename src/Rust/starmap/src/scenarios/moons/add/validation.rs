use anyhow::ensure;

use super::types::AddMoonScenarioInput;

const SHORT_NAME_ERROR: &str = "The moon name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str = "The moon description must be longer than 3 characters.";

pub async fn validate_add_moon_scenario_input(
    input: AddMoonScenarioInput,
) -> anyhow::Result<AddMoonScenarioInput> {
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
        let input = AddMoonScenarioInput {
            name: "Luna".to_string(),
            description: "Earth's moon".to_string(),
        };
        let result = validate_add_moon_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.name, input.clone().name);
        assert_eq!(result.description, input.clone().description);
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let input = AddMoonScenarioInput {
            name: "Hi".to_string(),
            description: "Earth's moon".to_string(),
        };
        let result = validate_add_moon_scenario_input(input).await.unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let input = AddMoonScenarioInput {
            name: "Luna".to_string(),
            description: "Hi".to_string(),
        };
        let result = validate_add_moon_scenario_input(input).await.unwrap_err();
        println!("{:?}", result);
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }

    #[tokio::test]
    async fn test_validation_trimmed_name() {
        let input = AddMoonScenarioInput {
            name: "  A  ".to_string(),
            description: "Earth's moon".to_string(),
        };
        let result = validate_add_moon_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_NAME_ERROR));
    }

    #[tokio::test]
    async fn test_validation_trimmed_description() {
        let input = AddMoonScenarioInput {
            name: "Luna".to_string(),
            description: "  Hi                ".to_string(),
        };
        let result = validate_add_moon_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }
}