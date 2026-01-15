use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::stars::add::add::add_star_scenario;
use starmap::scenarios::stars::add::types::AddStarScenarioInput;
use starmap::scenarios::stars::delete::delete::delete_star_scenario;
use starmap::scenarios::stars::delete::types::DeleteStarScenarioInput;

#[sqlx::test(migrations = "./../migrations")]
async fn test_delete_star_scenario_success(db_pool: PgPool) -> Result<()> {
    // First add a star
    let add_input = AddStarScenarioInput {
        name: "Sirius".to_string(),
        description: "Dog star".to_string(),
    };
    let added = add_star_scenario(add_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star");

    // Now delete it
    let delete_input = DeleteStarScenarioInput {
        id: added.id,
    };
    let result = delete_star_scenario(delete_input, Option::from(db_pool), None)
        .await;

    assert!(result.is_ok());
    Ok(())
}