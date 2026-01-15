use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::stars::add::add::add_star_scenario;
use starmap::scenarios::stars::add::types::AddStarScenarioInput;

#[sqlx::test(migrations = "./../migrations")]
async fn test_add_star_scenario_success(db_pool: PgPool) -> Result<()> {
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

#[sqlx::test(migrations = "./../migrations")]
async fn test_add_star_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    let input = AddStarScenarioInput {
        name: "Hi".to_string(),
        description: "Dog star".to_string(),
    };
    let result = add_star_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}