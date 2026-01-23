use super::types::DeleteStarScenarioInput;

/// Validates the input for deleting a star
pub fn validate_delete_star_scenario_input(input: &DeleteStarScenarioInput) -> anyhow::Result<()> {
    if input.id <= 0 {
        return Err(anyhow::anyhow!("The star id must be positive."));
    }
    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_validation_valid_id() {
        let input = DeleteStarScenarioInput { id: 1 };
        assert!(validate_delete_star_scenario_input(&input).is_ok());
    }

    #[test]
    fn test_validation_invalid_id_zero() {
        let input = DeleteStarScenarioInput { id: 0 };
        assert!(validate_delete_star_scenario_input(&input).is_err());
    }

    #[test]
    fn test_validation_invalid_id_negative() {
        let input = DeleteStarScenarioInput { id: -1 };
        assert!(validate_delete_star_scenario_input(&input).is_err());
    }
}
