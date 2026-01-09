#[cfg(test)]
mod planets_integration_tests {
    use encyclopedia_galactica::logic::starmap::scenarios::planets::add::add::add_planet_scenario;
    use encyclopedia_galactica::logic::starmap::scenarios::planets::add::types::AddPlanetScenarioInput;
    use sqlx::Pool;
    use sqlx::Postgres;
    use sqlx::postgres::PgPoolOptions;
    use std::fs;
    use toml::Value;

    async fn get_db_connection() -> Pool<Postgres> {
        let config_content = fs::read_to_string("eg.config.toml").expect("Failed to read config");
        let config: Value = toml::from_str(&config_content).expect("Failed to parse config");
        let conn_str = config["starmap_test"]["database_connection_string"]
            .as_str()
            .expect("Missing starmap_test.database_connection_string");
        PgPoolOptions::new()
            .max_connections(5)
            .connect(conn_str)
            .await
            .expect("Failed to connect to DB")
    }

    async fn setup_db(pool: &Pool<Postgres>) {
        sqlx::query(include_str!("../src/logic/starmap/database/planets.sql"))
            .execute(pool)
            .await
            .expect("Failed to create planets table");
    }

    async fn teardown_db(pool: &Pool<Postgres>) {
        sqlx::query("DROP TABLE IF EXISTS planets")
            .execute(pool)
            .await
            .expect("Failed to drop planets table");
    }

    #[tokio::test]
    async fn test_add_planet_scenario_success() {
        let pool = get_db_connection().await;
        setup_db(&pool).await;

        let input = AddPlanetScenarioInput {
            name: "Earth".to_string(),
            description: "Home planet".to_string(),
        };
        let conn_str = "postgres://eg:eg@localhost:5432/eg"; // from config, but hardcoded for test
        let result = add_planet_scenario(input, conn_str)
            .await
            .expect("Failed to add planet");

        assert_eq!(result.name, "Earth");
        assert_eq!(result.description, "Home planet");
        assert!(result.id > 0);

        teardown_db(&pool).await;
    }

    #[tokio::test]
    async fn test_add_planet_scenario_invalid_input() {
        let conn_str = "dummy"; // won't reach DB
        let input = AddPlanetScenarioInput {
            name: "Hi".to_string(),
            description: "Home planet".to_string(),
        };
        let result = add_planet_scenario(input, conn_str).await;
        assert!(result.is_err());
    }
}
