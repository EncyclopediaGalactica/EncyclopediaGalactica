use encyclopedia_galactica::logic::starmap::scenarios::planets::add::add::add_planet_scenario;
use encyclopedia_galactica::logic::starmap::scenarios::planets::add::types::AddPlanetScenarioInput;
use encyclopedia_galactica::logic::starmap::scenarios::planets::delete::delete::delete_planet_scenario;
use encyclopedia_galactica::logic::starmap::scenarios::planets::delete::types::DeletePlanetScenarioInput;

#[sqlx::test]
async fn test_delete_planet_scenario_success(pool: sqlx::PgPool) {
    // Add a planet first
    let add_input = AddPlanetScenarioInput {
        name: "Earth".to_string(),
        description: "Home planet".to_string(),
    };
    let add_result = add_planet_scenario(add_input, Some(pool.clone()), None)
        .await
        .unwrap();

    // Now delete it
    let delete_input = DeletePlanetScenarioInput { id: add_result.id };
    let result = delete_planet_scenario(delete_input, Some(pool), None).await;
    assert!(result.is_ok());
}
