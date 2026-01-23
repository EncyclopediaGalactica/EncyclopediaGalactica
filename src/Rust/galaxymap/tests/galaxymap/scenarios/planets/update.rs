use galaxymap::scenarios::planets::add::add::add_planet_scenario;
use galaxymap::scenarios::planets::add::types::AddPlanetScenarioInput;
use galaxymap::scenarios::planets::update::types::UpdatePlanetScenarioInput;
use galaxymap::scenarios::planets::update::update::update_planet_scenario;
use sqlx::Result;

#[sqlx::test(migrations = "./../migrations")]
async fn test_update_planet_scenario_success(pool: sqlx::PgPool) -> Result<()> {
    // First, add a planet to update
    let add_input = AddPlanetScenarioInput {
        data: serde_json::json!({"name": "Earth", "description": "Home planet"}),
    };
    let add_result = add_planet_scenario(add_input, Some(pool.clone()), None)
        .await
        .unwrap();
    let planet_id = add_result.id;

    // Now update it
    let update_input = UpdatePlanetScenarioInput {
        id: planet_id,
        data: serde_json::json!({"name": "Updated Earth", "description": "Updated home planet"}),
    };
    let update_result = update_planet_scenario(update_input, Some(pool), None)
        .await
        .unwrap();

    assert_eq!(update_result.id, planet_id);
    assert_eq!(update_result.data["name"], "Updated Earth");
    assert_eq!(update_result.data["description"], "Updated home planet");
    Ok(())
}

#[sqlx::test(migrations = "./../migrations")]
async fn test_update_planet_scenario_invalid_id(pool: sqlx::PgPool) -> Result<()> {
    let update_input = UpdatePlanetScenarioInput {
        id: 99999, // Non-existent ID
        data: serde_json::json!({"name": "Fake Planet", "description": "Does not exist"}),
    };
    let result = update_planet_scenario(update_input, Some(pool), None).await;
    assert!(result.is_err());
    let err = result.unwrap_err();
    assert!(err.to_string().contains("Id does not exist"));
    Ok(())
}

#[sqlx::test]
async fn test_update_planet_scenario_invalid_input_short_name(pool: sqlx::PgPool) -> Result<()> {
    let update_input = UpdatePlanetScenarioInput {
        id: 1,
        data: serde_json::json!({"name": "Ab", "description": "Valid description"}),
    };
    let result = update_planet_scenario(update_input, Some(pool), None).await;
    assert!(result.is_err());
    Ok(())
}
