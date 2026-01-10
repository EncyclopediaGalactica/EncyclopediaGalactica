use encyclopedia_galactica::logic::starmap::scenarios::planets::add::add::add_planet_scenario;
use encyclopedia_galactica::logic::starmap::scenarios::planets::add::types::AddPlanetScenarioInput;
use encyclopedia_galactica::logic::starmap::scenarios::planets::get_all::get_all::get_all_planets_scenario;
use sqlx::Result;

#[sqlx::test]
async fn test_get_all_planets_scenario_success(pool: sqlx::PgPool) -> Result<()> {
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
    assert_eq!(result.planets.len(), 2);

    // Assert that both planets are present with correct content
    let earth_planet = result.planets.iter().find(|p| p.name == "Earth").unwrap();
    assert_eq!(earth_planet.description, "Home planet");
    assert_eq!(earth_planet.id, add_result1.id);

    let mars_planet = result.planets.iter().find(|p| p.name == "Mars").unwrap();
    assert_eq!(mars_planet.description, "Red planet");
    assert_eq!(mars_planet.id, add_result2.id);
    Ok(())
}
