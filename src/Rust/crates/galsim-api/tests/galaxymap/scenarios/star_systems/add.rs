use gal_nav_api::star_systems::add::add::add_star_system_scenario;
use galsim_objects::star_system::scenario_entities::add_star_system_scenario_input::AddStarSystemScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_add_star_system_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddStarSystemScenarioInput {
        name: "Solar System".to_string(),
        description: "Home star system".to_string(),
        x: Some(1.0),
        y: Some(2.0),
        z: Some(3.0),
    };
    let result = add_star_system_scenario(input, Option::from(db_pool), None)
        .await
        .expect("Failed to add star system");

    assert_eq!(result.name, "Solar System");
    assert_eq!(result.description, "Home star system");
    assert!(result.id > 0);
    Ok(())
}

#[sqlx::test]
async fn test_add_star_system_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddStarSystemScenarioInput {
        name: "Hi".to_string(),
        description: "Home star system".to_string(),
        x: None,
        y: None,
        z: None,
    };
    let result = add_star_system_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
