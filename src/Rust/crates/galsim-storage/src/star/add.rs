use anyhow::Context;
use galsim_objects::star::entities::star_entity::StarEntity;
use log::debug;
use sqlx::PgPool;

pub async fn add_star(input: StarEntity, db_connection: PgPool) -> anyhow::Result<StarEntity> {
    let result: StarEntity = sqlx::query_as(
        r#"
        INSERT INTO
            stars (details)
            VALUES ($1)
        RETURNING id, details
        "#,
    )
    .bind(&input.details())
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert star: (data: {:?})", input.details()))?;

    debug!("Star table: entity inserted with id: {:?}", result.id());
    Ok(result)
}

#[cfg(test)]
mod tests {

    use super::*;
    use galsim_objects::star::entities::star_entity_details::StarEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_add_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // Add a star to test
        let details =
            StarEntityDetails::new("Test Star".to_string(), "Test Description".to_string());
        let add_input = StarEntity::new(0, Json(details));
        let added = add_star(add_input, pool.clone()).await.unwrap();

        assert!(added.id() > 0);
        assert_eq!(added.details().name(), "Test Star");
        assert_eq!(added.details().description(), "Test Description");
        Ok(())
    }
}
