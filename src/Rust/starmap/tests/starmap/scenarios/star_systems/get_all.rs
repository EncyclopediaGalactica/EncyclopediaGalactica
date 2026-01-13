use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::star_systems::add::add::add_star_system_scenario;
use starmap::scenarios::star_systems::add::types::AddStarSystemScenarioInput;
use starmap::scenarios::star_systems::get_all::get_all::get_all_star_systems_scenario;

#[sqlx::test(migrations = "./../migrations")]
async fn test_get_all_star_systems_scenario_success(db_pool: PgPool) -> Result<()> {
    // Add some star systems
    let add_input1 = AddStarSystemScenarioInput {
        name: "Star System 1".to_string(),
        description: "Description 1".to_string(),
    };
    let _ = add_star_system_scenario(add_input1, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star system");

    let add_input2 = AddStarSystemScenarioInput {
        name: "Star System 2".to_string(),
        description: "Description 2".to_string(),
    };
    let _ = add_star_system_scenario(add_input2, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star system");

    // Get all
    let all = get_all_star_systems_scenario(Option::from(db_pool), None)
        .await
        .expect("Failed to get all star systems");

    assert!(all.len() >= 2);
    // Check names
    let names: Vec<String> = all.iter().map(|s| s.name.clone()).collect();
    assert!(names.contains(&"Star System 1".to_string()));
    assert!(names.contains(&"Star System 2".to_string()));
    Ok(())
}
