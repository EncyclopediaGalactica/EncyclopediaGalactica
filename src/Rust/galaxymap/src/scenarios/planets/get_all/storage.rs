use anyhow::{Context, Result};
use log::debug;
use sqlx::{PgPool, Row, query};

use crate::scenarios::planets::PlanetEntity;

/// Retrieves all planets from the database
pub async fn get_all_from_storage(pool: &PgPool) -> Result<Vec<PlanetEntity>> {
    let planets: Vec<PlanetEntity> = query(
        r#"
    SELECT id, details
    FROM planets
    "#,
    )
    .fetch_all(pool)
    .await
    .with_context(|| "Failed to get all planets")?
    .into_iter()
    .map(|row| PlanetEntity {
        id: row.get(0),
        details: row.get(1),
    })
    .collect();

    debug!("Retrieved {} planets", planets.len());

    Ok(planets)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::planets::add::storage::add_to_storage;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_get_all_from_storage_returns_correct_count(pool: PgPool) -> sqlx::Result<()> {
        // Insert a few test planets
        let planet1 = PlanetEntity::new(
            0,
            serde_json::json!({"name": "Test Planet 1", "description": "Desc 1"}),
        );
        let planet2 = PlanetEntity::new(
            0,
            serde_json::json!({"name": "Test Planet 2", "description": "Desc 2"}),
        );

        add_to_storage(planet1, pool.clone()).await.unwrap();
        add_to_storage(planet2, pool.clone()).await.unwrap();

        // Retrieve all
        let result = get_all_from_storage(&pool).await.unwrap();

        // Assert that at least the inserted planets are present (DB may have more)
        assert!(result.len() >= 2);
        Ok(())
    }
}
