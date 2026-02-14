use gal_nav_api::planets::add::add::add_planet_scenario;
use galsim_objects::planet::scenario_entities::add_planet_scenario_input::AddPlanetScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_add_planet_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddPlanetScenarioInput {
        name: "Earth".to_string(),
        description: "Home planet".to_string(),
    };
    let result = add_planet_scenario(input, Option::from(db_pool), None)
        .await
        .expect("Failed to add planet");

    assert_eq!(result.name, "Earth");
    assert_eq!(result.description, "Home planet");
    assert!(result.id > 0);
    Ok(())
}

#[sqlx::test]
async fn test_add_planet_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddPlanetScenarioInput {
        name: "Hi".to_string(),
        description: "Home planet".to_string(),
    };
    let result = add_planet_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
