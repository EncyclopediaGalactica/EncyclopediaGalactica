use anyhow::ensure;
use galsim_objects::star_system::scenario_entities::delete_star_system_scenario_input::DeleteStarSystemScenarioInput;

const INVALID_ID_ERROR: &str = "The star system ID must be greater than 0.";

pub async fn validate_delete_star_system_scenario_input(
    input: DeleteStarSystemScenarioInput,
) -> anyhow::Result<DeleteStarSystemScenarioInput> {
    ensure!(input.id > 0, INVALID_ID_ERROR);
    Ok(input)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[tokio::test]
    async fn test_validation_valid_input() {
        let input = DeleteStarSystemScenarioInput { id: 1 };
        let result = validate_delete_star_system_scenario_input(input.clone())
            .await
            .unwrap();
        assert_eq!(result.id, input.id);
    }

    #[tokio::test]
    async fn test_validation_invalid_id() {
        let input = DeleteStarSystemScenarioInput { id: 0 };
        let result = validate_delete_star_system_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), INVALID_ID_ERROR);
    }

    #[tokio::test]
    async fn test_validation_invalid_id_negative() {
        let input = DeleteStarSystemScenarioInput { id: -1 };
        let result = validate_delete_star_system_scenario_input(input)
            .await
            .unwrap_err();
        assert_eq!(result.to_string(), INVALID_ID_ERROR);
    }
}
