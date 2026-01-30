use galaxymap::scenarios::planets::add::add::add_planet_scenario;
use galaxymap::scenarios::planets::add::types::AddPlanetScenarioInput;
use galaxymap::scenarios::planets::get_all::get_all::get_all_planets_scenario;
use sqlx::Result;

#[sqlx::test(migrations = "./../migrations")]
async fn test_get_all_planets_scenario_success(pool: sqlx::PgPool) -> Result<()> {
    // Add two planets for test data
    let add_input1 = AddPlanetScenarioInput {
        details: serde_json::json!({"name": "Earth", "description": "Home planet"}),
    };
    let add_result1 = add_planet_scenario(add_input1.clone(), Some(pool.clone()), None)
        .await
        .unwrap();

    let add_input2 = AddPlanetScenarioInput {
        details: serde_json::json!({"name": "Mars", "description": "Red planet"}),
    };
    let add_result2 = add_planet_scenario(add_input2.clone(), Some(pool.clone()), None)
        .await
        .unwrap();

    // Now get all planets
    let result = get_all_planets_scenario(Some(pool), None).await.unwrap();

    // Assert that we have exactly 2 planets
    assert!(result.len() > 2);

    // Assert that both planets are present with correct content
    let earth_planet = result.iter().find(|p| p.data["name"] == "Earth").unwrap();
    assert_eq!(earth_planet.data["description"], "Home planet");
    assert_eq!(earth_planet.id, add_result1.id);

    let mars_planet = result.iter().find(|p| p.data["name"] == "Mars").unwrap();
    assert_eq!(mars_planet.data["description"], "Red planet");
    assert_eq!(mars_planet.id, add_result2.id);
    Ok(())
}
