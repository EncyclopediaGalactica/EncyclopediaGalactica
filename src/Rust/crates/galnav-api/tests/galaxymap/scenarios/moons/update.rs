use gal_nav_api::moons::add::add::add_moon_scenario;
use gal_nav_api::moons::update::update::update_moon_scenario;
use galnav_objects::moon::scenario_entities::add_moon_scenario_input::AddMoonScenarioInput;
use galnav_objects::moon::scenario_entities::update_moon_scenario_input::UpdateMoonScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_update_moon_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&db_pool)
        .await
        .unwrap();
    // First add a moon
    let add_input = AddMoonScenarioInput {
        name: "Luna".to_string(),
        description: "Earth's moon".to_string(),
    };
    let added = add_moon_scenario(add_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add moon");

    // Now update it
    let update_input = UpdateMoonScenarioInput {
        id: added.id,
        name: "Updated Luna".to_string(),
        description: "Updated description".to_string(),
    };
    let result = update_moon_scenario(update_input, Option::from(db_pool), None)
        .await
        .expect("Failed to update moon");

    assert_eq!(result.id, added.id);
    assert_eq!(result.name, "Updated Luna");
    assert_eq!(result.description, "Updated description");
    Ok(())
}

#[sqlx::test]
async fn test_update_moon_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = UpdateMoonScenarioInput {
        id: 0,
        name: "Luna".to_string(),
        description: "Earth's moon".to_string(),
    };
    let result = update_moon_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
