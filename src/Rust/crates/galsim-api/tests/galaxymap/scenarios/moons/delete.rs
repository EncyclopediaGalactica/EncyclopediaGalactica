use galsim_api::moons::add::add::add_moon_scenario;
use galsim_api::moons::delete::delete::delete_moon_scenario;
use galsim_objects::moon::scenario_entities::add_moon_scenario_input::AddMoonScenarioInput;
use galsim_objects::moon::scenario_entities::delete_moon_scenario_input::DeleteMoonScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_delete_moon_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../galsim-storage/migrations")
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

    // Now delete it
    let delete_input = DeleteMoonScenarioInput { id: added.id };
    let result = delete_moon_scenario(delete_input, Option::from(db_pool), None).await;

    assert!(result.is_ok());
    Ok(())
}

#[sqlx::test]
async fn test_delete_moon_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../galsim-storage/migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let delete_input = DeleteMoonScenarioInput { id: 0 };
    let result = delete_moon_scenario(delete_input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
