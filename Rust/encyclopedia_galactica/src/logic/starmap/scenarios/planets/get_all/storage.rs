use anyhow::Result;
use log::debug;
use sqlx::{PgPool, Row, query};

use crate::logic::starmap::scenarios::planets::PlanetEntity;

/// Retrieves all planets from the database
pub async fn get_all_from_storage(pool: &PgPool) -> Result<Vec<PlanetEntity>> {
    let planets: Vec<PlanetEntity> = query("SELECT id, name, description FROM planets")
        .fetch_all(pool)
        .await?
        .into_iter()
        .map(|row| PlanetEntity {
            id: row.get(0),
            name: row.get(1),
            description: row.get(2),
        })
        .collect();

    debug!("Retrieved {} planets", planets.len());

    Ok(planets)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::logic::starmap::scenarios::planets::add::storage::add_to_storage;
    use sqlx::PgPool;

    #[sqlx::test]
    async fn test_get_all_from_storage_returns_correct_count(pool: PgPool) {
        // Insert a few test planets
        let planet1 = PlanetEntity::new(0, "Test Planet 1".to_string(), "Desc 1".to_string());
        let planet2 = PlanetEntity::new(0, "Test Planet 2".to_string(), "Desc 2".to_string());

        add_to_storage(planet1, pool.clone()).await.unwrap();
        add_to_storage(planet2, pool.clone()).await.unwrap();

        // Retrieve all
        let result = get_all_from_storage(&pool).await.unwrap();

        // Assert that at least the inserted planets are present (DB may have more)
        assert!(result.len() >= 2);
    }
}
