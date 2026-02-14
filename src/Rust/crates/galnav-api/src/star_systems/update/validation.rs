use anyhow::ensure;
use gal_nav_domain_objects::star_system::scenario_entities::update_star_system_scenario_input::UpdateStarSystemScenarioInput;

const INVALID_ID_ERROR: &str = "The star system ID must be greater than 0.";
const SHORT_NAME_ERROR: &str = "The star system name must be longer than 3 characters.";
const SHORT_DESCRIPTION_ERROR: &str =
    "The star system description must be longer than 3 characters.";

pub async fn validate_update_star_system_scenario_input(
    input: UpdateStarSystemScenarioInput,
) -> anyhow::Result<UpdateStarSystemScenarioInput> {
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
        let input = UpdateStarSystemScenarioInput {
            id: 1,
            name: "Solar System".to_string(),
            description: "Our home star system".to_string(),
            x: None,
            y: None,
            z: None,
        };
        let result = validate_update_star_system_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.id, input.id);
        assert_eq!(result.name, input.name);
        assert_eq!(result.description, input.description);
    }

    #[tokio::test]
    async fn test_validation_invalid_id() {
        let input = UpdateStarSystemScenarioInput {
            id: 0,
            name: "Solar System".to_string(),
            description: "Our home star system".to_string(),
            x: None,
            y: None,
            z: None,
        };
        let result = validate_update_star_system_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), INVALID_ID_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_name() {
        let input = UpdateStarSystemScenarioInput {
            id: 1,
            name: "Hi".to_string(),
            description: "Our home star system".to_string(),
            x: None,
            y: None,
            z: None,
        };
        let result = validate_update_star_system_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), SHORT_NAME_ERROR);
    }

    #[tokio::test]
    async fn test_validation_short_description() {
        let input = UpdateStarSystemScenarioInput {
            id: 1,
            name: "Solar System".to_string(),
            description: "Hi".to_string(),
            x: None,
            y: None,
            z: None,
        };
        let result = validate_update_star_system_scenario_input(input)
            .await
            .unwrap_err();
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_name() {
        let input = UpdateStarSystemScenarioInput {
            id: 1,
            name: "  A  ".to_string(),
            description: "Our home star system".to_string(),
            x: None,
            y: None,
            z: None,
        };
        let result = validate_update_star_system_scenario_input(input)
            .await
            .unwrap_err();
        assert!(result.to_string().contains(SHORT_NAME_ERROR));
    }

    #[tokio::test]
    async fn test_validation_short_trimmed_description() {
        let input = UpdateStarSystemScenarioInput {
            id: 1,
            name: "Solar System".to_string(),
            description: "  Hi                ".to_string(),
            x: None,
            y: None,
            z: None,
        };
        let result = validate_update_star_system_scenario_input(input)
            .await
            .unwrap_err();
        assert!(result.to_string().contains(SHORT_DESCRIPTION_ERROR));
    }
}
