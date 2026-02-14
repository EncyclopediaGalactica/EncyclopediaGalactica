use anyhow::ensure;
use galsim_objects::star_system::scenario_entities::get_star_system_coordinates_by_name_scenario_input::StarSystemCoordinatesByNameScenarioInput;

const NAME_IS_EMPTY: &str = "The star system name cannot be empty";

pub async fn validate_input(
    input: &StarSystemCoordinatesByNameScenarioInput,
) -> anyhow::Result<()> {
    ensure!(!input.name.is_empty(), NAME_IS_EMPTY);
    Ok(())
}
