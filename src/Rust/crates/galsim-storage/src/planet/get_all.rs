use anyhow::{Context, Result};
use galsim_objects::planet::entities::planet_entity::PlanetEntity;
use log::debug;
use sqlx::PgPool;

/// Retrieves all planets from the database
pub async fn get_all_planets(pool: &PgPool) -> Result<Vec<PlanetEntity>> {
    let planets: Vec<PlanetEntity> = sqlx::query_as::<_, PlanetEntity>(
        r#"
    SELECT 
        id, 
        details
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
    use crate::planet::add_planet::add_planet;

    use super::*;
    use galsim_objects::planet::entities::planet_entity_details::PlanetEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_get_all_from_storage_returns_correct_count(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../galsim-migrations")
            .run(&pool)
            .await
            .unwrap();
        // Insert a few test planets
        let details1 = PlanetEntityDetails::new("Test Planet 1".to_string(), "Desc 1".to_string());
        let planet1 = PlanetEntity::new(0, Json(details1));
        let details2 = PlanetEntityDetails::new("Test Planet 2".to_string(), "Desc 2".to_string());
        let planet2 = PlanetEntity::new(0, Json(details2));

        add_planet(planet1, pool.clone()).await.unwrap();
        add_planet(planet2, pool.clone()).await.unwrap();

        // Retrieve all
        let result = get_all_planets(&pool).await.unwrap();

        // Assert that at least the inserted planets are present (DB may have more)
        assert!(result.len() >= 2);
        Ok(())
    }
}
