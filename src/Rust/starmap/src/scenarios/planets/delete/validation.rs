use super::types::DeletePlanetScenarioInput;

/// Validates the input for deleting a planet
pub fn validate_delete_planet_scenario_input(
    input: &DeletePlanetScenarioInput,
) -> anyhow::Result<()> {
    if input.id <= 0 {
        return Err(anyhow::anyhow!("The planet id must be positive."));
    }
    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_validation_valid_id() {
        let input = DeletePlanetScenarioInput { id: 1 };
        assert!(validate_delete_planet_scenario_input(&input).is_ok());
    }

    #[test]
    fn test_validation_invalid_id_zero() {
        let input = DeletePlanetScenarioInput { id: 0 };
        assert!(validate_delete_planet_scenario_input(&input).is_err());
    }

    #[test]
    fn test_validation_invalid_id_negative() {
        let input = DeletePlanetScenarioInput { id: -1 };
        assert!(validate_delete_planet_scenario_input(&input).is_err());
    }
}
