use anyhow::ensure;

use super::types::AddPlanetScenarioInput;

const SHORT_NAME_ERROR: &str = "The planet name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str = "The planet description must be longer than 3 characters.";

pub async fn validate_add_planet_scenario_input(
    input: AddPlanetScenarioInput,
) -> anyhow::Result<AddPlanetScenarioInput> {
    println!("the input: {:?}", input);

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
        let input = AddPlanetScenarioInput {
            name: "Earth".to_string(),
            description: "Home planet".to_string(),
        };
        let result = validate_add_planet_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.name, "Earth");
        assert_eq!(result.description, "Home planet");
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let input = AddPlanetScenarioInput {
            name: "Hi".to_string(),
            description: "Home planet".to_string(),
        };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let input = AddPlanetScenarioInput {
            name: "Earth".to_string(),
            description: "Hi".to_string(),
        };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        println!("{:?}", result);
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_name() {
        let input = AddPlanetScenarioInput {
            name: "  A  ".to_string(),
            description: "Home planet".to_string(),
        };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_NAME_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_description() {
        let input = AddPlanetScenarioInput {
            name: "Earth".to_string(),
            description: "  Hi                ".to_string(),
        };
        let result = validate_add_planet_scenario_input(input).await.unwrap_err();
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }
}
