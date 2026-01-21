use sqlx::PgPool;
use sqlx::Result;
use starmap::scenarios::stars::add::add::add_star_scenario;
use starmap::scenarios::stars::add::types::AddStarScenarioInput;
use starmap::scenarios::stars::get_all::get_all::get_all_stars_scenario;
use starmap::scenarios::stars::get_all::types::GetAllStarsScenarioInput;

#[sqlx::test(migrations = "./../migrations")]
async fn test_get_all_stars_scenario_success(db_pool: PgPool) -> Result<()> {
    // First add some stars
    let add_input1 = AddStarScenarioInput {
        name: "Sirius".to_string(),
        description: "Dog star".to_string(),
    };
    let add_input2 = AddStarScenarioInput {
        name: "Vega".to_string(),
        description: "Bright star".to_string(),
    };
    add_star_scenario(add_input1, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star 1");
    add_star_scenario(add_input2, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star 2");

    // Now get all
    let input = GetAllStarsScenarioInput {};
    let result = get_all_stars_scenario(input, Option::from(db_pool), None)
        .await
        .expect("Failed to get all stars");

    assert!(result.len() == 2);
    // Check that our stars are there
    let sirius_exists = result.iter().any(|s| s.name == "Sirius");
    let vega_exists = result.iter().any(|s| s.name == "Vega");
    assert!(sirius_exists);
    assert!(vega_exists);
    Ok(())
}
