use anyhow::Context;
use galnav_objects::moon::entities::moon_entity::MoonEntity;
use log::debug;
use sqlx::PgPool;

pub async fn add_moon(input: MoonEntity, db_connection: PgPool) -> anyhow::Result<MoonEntity> {
    let result: MoonEntity = sqlx::query_as(
        r#"
        INSERT INTO
            moons (details)
            VALUES ($1)
        RETURNING 
            id, 
            details
        "#,
    )
    .bind(input.details())
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert moon: (data: {:?}", input.details()))?;

    debug!("Moon table: entity inserted with id: {:?}", result.id());
    Ok(result)
}

#[cfg(test)]
mod tests {
    use galnav_objects::moon::entities::moon_entity::MoonEntity;
    use galnav_objects::moon::entities::moon_entity_details::MoonEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    use crate::moon::add::add_moon;

    #[sqlx::test]
    async fn test_add_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // Add a moon to test
        let data = MoonEntityDetails::new("Test Moon".to_string(), "Test Description".to_string());
        let add_input = MoonEntity::new(0, Json(data));
        let added = add_moon(add_input, pool.clone()).await.unwrap();

        assert_eq!(added.id(), added.id());
        assert_eq!(added.details().name(), "Test Moon");
        assert_eq!(added.details().description(), "Test Description");
        Ok(())
    }
}
