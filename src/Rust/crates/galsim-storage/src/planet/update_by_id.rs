use anyhow::{Context, Result};
use galsim_objects::planet::entities::planet_entity::PlanetEntity;
use galsim_objects::planet::entities::planet_entity_details::PlanetEntityDetails;
use log::debug;
use sqlx::{PgPool, Row, types::Json};

pub async fn update_by_id(pool: &PgPool, input: PlanetEntity) -> Result<PlanetEntity> {
    // First, check if the planet exists
    let exists: Option<i64> = sqlx::query(
        r#"
        SELECT 
            id 
        FROM 
            planets 
        WHERE 
            id = $1
    "#,
    )
    .bind(input.id())
    .fetch_optional(pool)
    .await?
    .map(|row| row.get(0));

    if exists.is_none() {
        return Err(anyhow::anyhow!("Id does not exist"));
    }

    // Perform the update
    let details = PlanetEntityDetails::new(
        input.details().name().to_string(),
        input.details().description().to_string(),
    );
    let json_details = Json(details);
    let row = sqlx::query(
        r#"
        UPDATE 
            planets
        SET 
            details = $2
        WHERE 
            id = $1
        RETURNING 
            id, 
            details
        "#,
    )
    .bind(input.id())
    .bind(&json_details)
    .fetch_one(pool)
    .await
    .with_context(|| "Failed to update planet")?;

    let entity = PlanetEntity::new(row.get(0), row.get(1));

    debug!("Updated planet: {:?}", entity);

    Ok(entity)
}

#[cfg(test)]
mod tests {
    use crate::planet::add_planet::add_planet;

    use super::*;
    use sqlx::PgPool;

    #[sqlx::test]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../galsim-migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add a planet to have an existing ID
        let add_details = PlanetEntityDetails::new(
            "Original Planet".to_string(),
            "Original Description".to_string(),
        );
        let add_input = PlanetEntity::new(0, Json(add_details));
        let added = add_planet(add_input, pool.clone()).await.unwrap();

        // Now update it
        let update_input = PlanetEntity::new(
            added.id(),
            Json(PlanetEntityDetails::new(
                "Updated Planet".to_string(),
                "Updated Description".to_string(),
            )),
        );
        let result = update_by_id(&pool, update_input).await.unwrap();

        assert_eq!(result.id(), added.id());
        assert_eq!(result.details().name(), "Updated Planet");
        assert_eq!(result.details().description(), "Updated Description");
        Ok(())
    }
}
