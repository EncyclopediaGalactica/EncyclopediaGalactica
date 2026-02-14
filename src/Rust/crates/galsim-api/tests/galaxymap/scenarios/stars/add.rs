use galsim_api::stars::add::add::add_star_scenario;
use galsim_objects::star::scenario_entities::add_star_scenario_input::AddStarScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_add_star_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../galsim-storage/migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddStarScenarioInput {
        name: "Sirius".to_string(),
        description: "Dog star".to_string(),
    };
    let result = add_star_scenario(input, Option::from(db_pool), None)
        .await
        .expect("Failed to add star");

    assert_eq!(result.name, "Sirius");
    assert_eq!(result.description, "Dog star");
    assert!(result.id > 0);
    Ok(())
}

#[sqlx::test]
async fn test_add_star_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../galsim-storage/migrations")
        .run(&db_pool)
        .await
        .unwrap();
    let input = AddStarScenarioInput {
        name: "Hi".to_string(),
        description: "Dog star".to_string(),
    };
    let result = add_star_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
