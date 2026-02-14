use gal_nav_api::planets::add::add::add_planet_scenario;
use gal_nav_api::planets::update::update::update_planet_scenario;
use galsim_objects::planet::scenario_entities::add_planet_scenario_input::AddPlanetScenarioInput;
use galsim_objects::planet::scenario_entities::update_planet_scenario_input::UpdatePlanetScenarioInput;
use sqlx::Result;

#[sqlx::test]
async fn test_update_planet_scenario_success(pool: sqlx::PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&pool)
        .await
        .unwrap();
    // First, add a planet to update
    let add_input = AddPlanetScenarioInput {
        name: "Earth".to_string(),
        description: "Home planet".to_string(),
    };
    let add_result = add_planet_scenario(add_input, Some(pool.clone()), None)
        .await
        .unwrap();
    let planet_id = add_result.id;

    // Now update it
    let update_input = UpdatePlanetScenarioInput {
        id: planet_id,
        name: "Updated Earth".to_string(),
        description: "Updated home planet".to_string(),
    };
    let update_result = update_planet_scenario(update_input, Some(pool), None)
        .await
        .unwrap();

    assert_eq!(update_result.id, planet_id);
    assert_eq!(update_result.name, "Updated Earth");
    assert_eq!(update_result.description, "Updated home planet");
    Ok(())
}

#[sqlx::test]
async fn test_update_planet_scenario_invalid_id(pool: sqlx::PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&pool)
        .await
        .unwrap();
    let update_input = UpdatePlanetScenarioInput {
        id: 99999, // Non-existent ID
        name: "Fake Planet".to_string(),
        description: "Does not exist".to_string(),
    };
    let result = update_planet_scenario(update_input, Some(pool), None).await;
    assert!(result.is_err());
    let err = result.unwrap_err();
    assert!(err.to_string().contains("Id does not exist"));
    Ok(())
}

#[sqlx::test]
async fn test_update_planet_scenario_invalid_input_short_name(pool: sqlx::PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&pool)
        .await
        .unwrap();
    let update_input = UpdatePlanetScenarioInput {
        id: 1,
        name: "Ab".to_string(),
        description: "Valid description".to_string(),
    };
    let result = update_planet_scenario(update_input, Some(pool), None).await;
    assert!(result.is_err());
    Ok(())
}
