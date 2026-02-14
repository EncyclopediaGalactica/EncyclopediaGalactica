use gal_nav_api::stars::add::add::add_star_scenario;
use gal_nav_api::stars::delete::delete::delete_star_scenario;
use galsim_objects::star::scenario_entities::add_star_scenario_input::AddStarScenarioInput;
use galsim_objects::star::scenario_entities::delete_star_scenario_input::DeleteStarScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_delete_star_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    // First add a star
    let add_input = AddStarScenarioInput {
        name: "Sirius".to_string(),
        description: "Dog star".to_string(),
    };
    let added = add_star_scenario(add_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star");

    // Now delete it
    let delete_input = DeleteStarScenarioInput { id: added.id };
    let result = delete_star_scenario(delete_input, Option::from(db_pool), None).await;

    assert!(result.is_ok());
    Ok(())
}

#[sqlx::test]
async fn test_delete_star_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let delete_input = DeleteStarScenarioInput { id: 0 };
    let result = delete_star_scenario(delete_input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
