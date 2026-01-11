#[cfg(test)]
mod star_systems_integration_tests {
    use encyclopedia_galactica::logic::starmap::scenarios::star_systems::add::add::add_star_system_scenario;
    use encyclopedia_galactica::logic::starmap::scenarios::star_systems::add::types::AddStarSystemScenarioInput;
    use sqlx::PgPool;
    use sqlx::Result;

    #[sqlx::test]
    async fn test_add_star_system_scenario_success(db_pool: PgPool) -> Result<()> {
        let input = AddStarSystemScenarioInput {
            name: "Solar System".to_string(),
            description: "Home star system".to_string(),
        };
        let result = add_star_system_scenario(input, Option::from(db_pool), None)
            .await
            .expect("Failed to add star system");

        assert_eq!(result.name, "Solar System");
        assert_eq!(result.description, "Home star system");
        assert!(result.id > 0);
        Ok(())
    }

    #[sqlx::test]
    async fn test_add_star_system_scenario_invalid_input(db_pool: PgPool) -> Result<()> {
        let input = AddStarSystemScenarioInput {
            name: "Hi".to_string(),
            description: "Home star system".to_string(),
        };
        let result = add_star_system_scenario(input, Option::from(db_pool), None).await;
        assert!(result.is_err());
        Ok(())
    }
}
