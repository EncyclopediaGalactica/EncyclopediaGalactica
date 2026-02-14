use gal_nav_api::moons::add::add::add_moon_scenario;
use gal_nav_api::moons::get_all::get_all::get_all_moons_scenario;
use galnav_objects::moon::scenario_entities::add_moon_scenario_input::AddMoonScenarioInput;
use galnav_objects::moon::scenario_entities::get_all_moons_scenario_input::GetAllMoonsScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_get_all_moons_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../../../migrations")
        .run(&db_pool)
        .await
        .unwrap();
    // First add some moons
    let add_input1 = AddMoonScenarioInput {
        name: "Luna".to_string(),
        description: "Earth's moon".to_string(),
    };
    let add_input2 = AddMoonScenarioInput {
        name: "Titan".to_string(),
        description: "Saturn's moon".to_string(),
    };
    add_moon_scenario(add_input1, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add moon 1");
    add_moon_scenario(add_input2, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add moon 2");

    // Now get all
    let input = GetAllMoonsScenarioInput {};
    let result = get_all_moons_scenario(input, Option::from(db_pool), None)
        .await
        .expect("Failed to get all moons");

    assert!(result.len() == 2);
    // Check that our moons are there
    let luna_exists = result.iter().any(|m| m.name == "Luna");
    let titan_exists = result.iter().any(|m| m.name == "Titan");
    assert!(luna_exists);
    assert!(titan_exists);
    Ok(())
}
