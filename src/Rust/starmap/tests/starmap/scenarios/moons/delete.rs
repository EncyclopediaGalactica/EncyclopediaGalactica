use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::moons::add::add::add_moon_scenario;
use starmap::scenarios::moons::add::types::AddMoonScenarioInput;
use starmap::scenarios::moons::delete::delete::delete_moon_scenario;
use starmap::scenarios::moons::delete::types::DeleteMoonScenarioInput;

#[sqlx::test(migrations = "./../migrations")]
async fn test_delete_moon_scenario_success(db_pool: PgPool) -> Result<()> {
    // First add a moon
    let add_input = AddMoonScenarioInput {
        name: "Luna".to_string(),
        description: "Earth's moon".to_string(),
    };
    let added = add_moon_scenario(add_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add moon");

    // Now delete it
    let delete_input = DeleteMoonScenarioInput {
        id: added.id,
    };
    let result = delete_moon_scenario(delete_input, Option::from(db_pool), None)
        .await;

    assert!(result.is_ok());
    Ok(())
}