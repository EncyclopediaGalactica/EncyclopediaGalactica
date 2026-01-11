use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::planets::add::add::add_planet_scenario;
use starmap::scenarios::planets::add::types::AddPlanetScenarioInput;

#[sqlx::test]
async fn test_add_planet_scenario_success(db_pool: PgPool) -> Result<()> {
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
    let input = AddPlanetScenarioInput {
        name: "Hi".to_string(),
        description: "Home planet".to_string(),
    };
    let result = add_planet_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
