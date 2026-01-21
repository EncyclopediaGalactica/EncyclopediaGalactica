use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::moons::add::add::add_moon_scenario;
use starmap::scenarios::moons::add::types::AddMoonScenarioInput;
use starmap::scenarios::moons::update::types::UpdateMoonScenarioInput;
use starmap::scenarios::moons::update::update::update_moon_scenario;

#[sqlx::test(migrations = "./../migrations")]
async fn test_update_moon_scenario_success(db_pool: PgPool) -> Result<()> {
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

#[sqlx::test(migrations = "./../migrations")]
async fn test_update_moon_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    let input = UpdateMoonScenarioInput {
        id: 0,
        name: "Luna".to_string(),
        description: "Earth's moon".to_string(),
    };
    let result = update_moon_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}
