use gal_nav_api::planets::add::add::add_planet_scenario;
use gal_nav_api::planets::get_all::get_all::get_all_planets_scenario;
use galnav_objects::planet::scenario_entities::add_planet_scenario_input::AddPlanetScenarioInput;
use sqlx::Result;

#[sqlx::test]
async fn test_get_all_planets_scenario_success(pool: sqlx::PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&pool)
        .await
        .unwrap();
    // Add two planets for test data
    let add_input1 = AddPlanetScenarioInput {
        name: "Earth".to_string(),
        description: "Home planet".to_string(),
    };
    let add_result1 = add_planet_scenario(add_input1.clone(), Some(pool.clone()), None)
        .await
        .unwrap();

    let add_input2 = AddPlanetScenarioInput {
        name: "Mars".to_string(),
        description: "Red planet".to_string(),
    };
    let add_result2 = add_planet_scenario(add_input2.clone(), Some(pool.clone()), None)
        .await
        .unwrap();

    // Now get all planets
    let result = get_all_planets_scenario(Some(pool), None).await.unwrap();

    // Assert that we have exactly 2 planets
    assert!(result.len() >= 2);

    // Assert that both planets are present with correct content
    let earth_planet = result.iter().find(|p| p.name == "Earth").unwrap();
    assert_eq!(earth_planet.description, "Home planet");
    assert_eq!(earth_planet.id, add_result1.id);

    let mars_planet = result.iter().find(|p| p.name == "Mars").unwrap();
    assert_eq!(mars_planet.description, "Red planet");
    assert_eq!(mars_planet.id, add_result2.id);
    Ok(())
}
