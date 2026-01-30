use anyhow::ensure;

use super::types::StarSystemCoordinatesByNameScenarioInput;

const NAME_IS_EMPTY: &str = "The star system name cannot be empty";
pub async fn validate_input(
    input: &StarSystemCoordinatesByNameScenarioInput,
) -> anyhow::Result<()> {
    ensure!(!input.name.is_empty(), NAME_IS_EMPTY);
    Ok(())
}
