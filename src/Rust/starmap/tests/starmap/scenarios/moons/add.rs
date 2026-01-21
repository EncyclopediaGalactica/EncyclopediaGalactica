use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::moons::add::add::add_moon_scenario;
use starmap::scenarios::moons::add::types::AddMoonScenarioInput;

#[sqlx::test(migrations = "./../migrations")]
async fn test_add_moon_scenario_success(db_pool: PgPool) -> Result<()> {
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

#[sqlx::test(migrations = "./../migrations")]
async fn test_add_moon_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    let input = AddMoonScenarioInput {
        name: "Hi".to_string(),
        description: "Earth's moon".to_string(),
    };
    let result = add_moon_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
