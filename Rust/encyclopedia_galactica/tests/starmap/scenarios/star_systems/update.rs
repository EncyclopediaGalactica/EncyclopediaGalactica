#[cfg(test)]
mod star_systems_integration_tests {
    use encyclopedia_galactica::logic::starmap::scenarios::star_systems::add::add::add_star_system_scenario;
    use encyclopedia_galactica::logic::starmap::scenarios::star_systems::add::types::AddStarSystemScenarioInput;
    use encyclopedia_galactica::logic::starmap::scenarios::star_systems::update::types::UpdateStarSystemScenarioInput;
    use encyclopedia_galactica::logic::starmap::scenarios::star_systems::update::update::update_star_system_scenario;
    use sqlx::PgPool;
    use sqlx::Result;

    #[sqlx::test]
    async fn test_update_star_system_scenario_success(db_pool: PgPool) -> Result<()> {
        // First add a star system
        let add_input = AddStarSystemScenarioInput {
            name: "Original Star System".to_string(),
            description: "Original Description".to_string(),
        };
        let added = add_star_system_scenario(add_input, Option::from(db_pool.clone()), None)
            .await
            .expect("Failed to add star system");

        // Now update it
        let update_input = UpdateStarSystemScenarioInput {
            id: added.id,
            name: "Updated Star System".to_string(),
            description: "Updated Description".to_string(),
        };
        let updated = update_star_system_scenario(update_input, Option::from(db_pool), None)
            .await
            .expect("Failed to update star system");

        assert_eq!(updated.id, added.id);
        assert_eq!(updated.name, "Updated Star System");
        assert_eq!(updated.description, "Updated Description");
        Ok(())
    }

    #[sqlx::test]
    async fn test_update_star_system_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
        let update_input = UpdateStarSystemScenarioInput {
            id: 0,
            name: "Updated Star System".to_string(),
            description: "Updated Description".to_string(),
        };
        let result = update_star_system_scenario(update_input, Option::from(db_pool), None).await;
        assert!(result.is_err());
        Ok(())
    }
}
