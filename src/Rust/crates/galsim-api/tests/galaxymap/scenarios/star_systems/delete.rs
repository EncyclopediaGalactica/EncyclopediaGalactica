use galsim_api::star_systems::add::add::add_star_system_scenario;
use galsim_api::star_systems::delete::delete::delete_star_system_scenario;
use galsim_api::star_systems::get_all::get_all::get_all_star_systems_scenario;
use galsim_objects::star_system::scenario_entities::add_star_system_scenario_input::AddStarSystemScenarioInput;
use galsim_objects::star_system::scenario_entities::delete_star_system_scenario_input::DeleteStarSystemScenarioInput;
use sqlx::PgPool;
use sqlx::Result;

#[sqlx::test]
async fn test_delete_star_system_scenario_success(db_pool: PgPool) -> Result<()> {
    sqlx::migrate!("./../galsim-storage/migrations")
        .run(&db_pool)
        .await
        .unwrap();
    // First add a star system
    let add_input = AddStarSystemScenarioInput {
        name: "Star System to Delete".to_string(),
        description: "Description".to_string(),
        x: Some(0.0),
        y: Some(0.0),
        z: Some(0.0),
    };
    let added = add_star_system_scenario(add_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to add star system");

    // Now delete it
    let delete_input = DeleteStarSystemScenarioInput { id: added.id };
    delete_star_system_scenario(delete_input, Option::from(db_pool.clone()), None)
        .await
        .expect("Failed to delete star system");

    // Verify deletion
    let all = get_all_star_systems_scenario(Option::from(db_pool), None)
        .await
        .expect("Failed to get all star systems");

    let ids: Vec<i64> = all.iter().map(|s| s.id).collect();
    assert!(!ids.contains(&added.id));
    Ok(())
}
