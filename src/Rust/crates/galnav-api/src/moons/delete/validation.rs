use galnav_objects::moon::scenario_entities::delete_moon_scenario_input::DeleteMoonScenarioInput;

pub fn validate_delete_moon_scenario_input(input: &DeleteMoonScenarioInput) -> anyhow::Result<()> {
    if input.id <= 0 {
        return Err(anyhow::anyhow!("The moon id must be positive."));
    }
    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_validation_valid_id() {
        let input = DeleteMoonScenarioInput { id: 1 };
        assert!(validate_delete_moon_scenario_input(&input).is_ok());
    }

    #[test]
    fn test_validation_invalid_id_zero() {
        let input = DeleteMoonScenarioInput { id: 0 };
        assert!(validate_delete_moon_scenario_input(&input).is_err());
    }

    #[test]
    fn test_validation_invalid_id_negative() {
        let input = DeleteMoonScenarioInput { id: -1 };
        assert!(validate_delete_moon_scenario_input(&input).is_err());
    }
}
