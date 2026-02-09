use gal_nav_api::star_systems::add::add::add_star_system_scenario;
use gal_nav_api::star_systems::get_all::get_all::get_all_star_systems_scenario;
use gal_nav_domain_objects::star_system::scenario_entities::add_star_system_scenario_input::AddStarSystemScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_get_all_star_systems_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&db_pool)
        .await
        .unwrap();
    // Add some star systems
    let add_input1 = AddStarSystemScenarioInput {
        name: "Star System 1".to_string(),
        description: "Description 1".to_string(),
        x: Some(1.0),
        y: Some(3.0),
        z: Some(2.0),
    };
    let _ = add_star_system_scenario(add_input1, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star system");

    let add_input2 = AddStarSystemScenarioInput {
        name: "Star System 2".to_string(),
        description: "Description 2".to_string(),
        x: Some(2.0),
        y: Some(3.0),
        z: Some(4.0),
    };
    let _r = add_star_system_scenario(add_input2, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star system");

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
