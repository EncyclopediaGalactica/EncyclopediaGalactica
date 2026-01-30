use anyhow::{Context, Result};
use log::debug;
use sqlx::PgPool;
use sqlx::types::Json;

use crate::scenarios::planets::PlanetEntity;
use crate::scenarios::planets::PlanetEntityDetails;

/// Retrieves all planets from the database
pub async fn get_all_from_storage(pool: &PgPool) -> Result<Vec<PlanetEntity>> {
    let planets: Vec<PlanetEntity> = sqlx::query_as!(
        PlanetEntity,
        r#"
    SELECT 
        id, 
        details as "details: Json<PlanetEntityDetails>"
    FROM 
         planets
    "#,
    )
    .fetch_all(pool)
    .await
    .with_context(|| "Failed to get all planets")?;

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
        let details1 = PlanetEntityDetails::new("Test Planet 1".to_string(), "Desc 1".to_string());
        let planet1 = PlanetEntity::new(0, Json(details1));
        let details2 = PlanetEntityDetails::new("Test Planet 2".to_string(), "Desc 2".to_string());
        let planet2 = PlanetEntity::new(0, Json(details2));

        add_to_storage(planet1, pool.clone()).await.unwrap();
        add_to_storage(planet2, pool.clone()).await.unwrap();

        // Retrieve all
        let result = get_all_from_storage(&pool).await.unwrap();

        // Assert that at least the inserted planets are present (DB may have more)
        assert!(result.len() >= 2);
        Ok(())
    }
}
