use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::stars::add::add::add_star_scenario;
use starmap::scenarios::stars::add::types::AddStarScenarioInput;
use starmap::scenarios::stars::update::types::UpdateStarScenarioInput;
use starmap::scenarios::stars::update::update::update_star_scenario;

#[sqlx::test(migrations = "./../migrations")]
async fn test_update_star_scenario_success(db_pool: PgPool) -> Result<()> {
    // First add a star
    let add_input = AddStarScenarioInput {
        name: "Sirius".to_string(),
        description: "Dog star".to_string(),
    };
    let added = add_star_scenario(add_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star");

    // Now update it
    let update_input = UpdateStarScenarioInput {
        id: added.id,
        name: "Updated Sirius".to_string(),
        description: "Updated description".to_string(),
    };
    let result = update_star_scenario(update_input, Option::from(db_pool), None)
        .await
        .expect("Failed to update star");

    assert_eq!(result.id, added.id);
    assert_eq!(result.name, "Updated Sirius");
    assert_eq!(result.description, "Updated description");
    Ok(())
}

#[sqlx::test(migrations = "./../migrations")]
async fn test_update_star_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
    let input = UpdateStarScenarioInput {
        id: 0,
        name: "Sirius".to_string(),
        description: "Dog star".to_string(),
    };
    let result = update_star_scenario(input, Option::from(db_pool), None).await;
    assert!(result.is_err());
    Ok(())
}

