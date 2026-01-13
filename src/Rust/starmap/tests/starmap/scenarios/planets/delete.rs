use sqlx::Result;
use starmap::scenarios::planets::add::add::add_planet_scenario;
use starmap::scenarios::planets::add::types::AddPlanetScenarioInput;
use starmap::scenarios::planets::delete::delete::delete_planet_scenario;
use starmap::scenarios::planets::delete::types::DeletePlanetScenarioInput;

#[sqlx::test(migrations = "./../migrations")]
async fn test_delete_planet_scenario_success(pool: sqlx::PgPool) -> Result<()> {
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
    delete_planet_scenario(delete_input, Some(pool), None)
        .await
        .expect("Failed to delete planet");
    Ok(())
}
