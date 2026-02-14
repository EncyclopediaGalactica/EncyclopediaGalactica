use gal_nav_api::star_systems::add::add::add_star_system_scenario;
use gal_nav_api::star_systems::update::update::update_star_system_scenario;
use galsim_objects::star_system::scenario_entities::add_star_system_scenario_input::AddStarSystemScenarioInput;
use galsim_objects::star_system::scenario_entities::update_star_system_scenario_input::UpdateStarSystemScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_update_star_system_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    // First add a star system
    let add_input = AddStarSystemScenarioInput {
        name: "Original Star System".to_string(),
        description: "Original Description".to_string(),
        x: Some(0.0),
        y: Some(0.0),
        z: Some(0.0),
    };
    let added = add_star_system_scenario(add_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star system");

    // Now update it
    let update_input = UpdateStarSystemScenarioInput {
        id: added.id,
        name: "Updated Star System".to_string(),
        description: "Updated Description".to_string(),
        x: Some(4.0),
        y: Some(5.0),
        z: Some(6.0),
    };
    let updated = update_star_system_scenario(update_input, Option::from(db_pool), None)
        .await
        .expect("Failed to update star system");

    assert_eq!(updated.id, added.id);
    assert_eq!(updated.name, "Updated Star System");
    assert_eq!(updated.description, "Updated Description");
    Ok(())
}

#[sqlx::test]
async fn test_update_star_system_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let update_input = UpdateStarSystemScenarioInput {
        id: 0,
        name: "Updated Star System".to_string(),
        description: "Updated Description".to_string(),
        x: None,
        y: None,
        z: None,
    };
    let result = update_star_system_scenario(update_input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
