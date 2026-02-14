use gal_nav_api::planets::add::add::add_planet_scenario;
use gal_nav_api::planets::delete::delete::delete_planet_scenario;
use galnav_objects::planet::scenario_entities::add_planet_scenario_input::AddPlanetScenarioInput;
use galnav_objects::planet::scenario_entities::delete_planet_scenario_input::DeletePlanetScenarioInput;
use sqlx::Result;

#[sqlx::test]
async fn test_delete_planet_scenario_success(pool: sqlx::PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&pool)
        .await
        .unwrap();
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
