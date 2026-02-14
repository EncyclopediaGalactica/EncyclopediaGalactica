use gal_nav_api::moons::add::add::add_moon_scenario;
use galsim_objects::moon::scenario_entities::add_moon_scenario_input::AddMoonScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_add_moon_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddMoonScenarioInput {
        name: "Luna".to_string(),
        description: "Earth's moon".to_string(),
    };
    let result = add_moon_scenario(input, Option::from(db_pool), None)
        .await
        .expect("Failed to add moon");

    assert_eq!(result.name, "Luna");
    assert_eq!(result.description, "Earth's moon");
    assert!(result.id > 0);
    Ok(())
}

#[sqlx::test]
async fn test_add_moon_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddMoonScenarioInput {
        name: "Hi".to_string(),
        description: "Earth's moon".to_string(),
    };
    let result = add_moon_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
