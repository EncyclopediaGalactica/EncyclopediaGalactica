use anyhow::Context;
use galsim_objects::star_system::entities::star_system::StarSystemEntity;
use log::debug;
use sqlx::PgPool;

pub async fn add_star_system(
    input: StarSystemEntity,
    db_connection: PgPool,
) -> anyhow::Result<StarSystemEntity> {
    let result: StarSystemEntity = sqlx::query_as(
        r#"
        INSERT INTO
            star_systems (details)
            VALUES ($1)
        RETURNING 
            id, 
            details
        "#,
    )
    .bind(&input.details())
    .fetch_one(&db_connection)
    .await
    .with_context(|| {
        format!(
            "Failed to insert star system: (data: {:?})",
            input.details()
        )
    })?;

    debug!(
        "Star system table: entity inserted with id: {:?}",
        result.id()
    );
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use galsim_objects::star_system::entities::star_system_details::StarSystemEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_add_to_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add a star system
        let data = StarSystemEntityDetails::new(
            "Original Star System".to_string(),
            "Original Description".to_string(),
            Some(0.0),
            Some(0.0),
            Some(0.0),
        );
        let add_input = StarSystemEntity::new(0, Json(data));
        let added = add_star_system(add_input, pool.clone()).await.unwrap();

        assert_eq!(added.id(), added.id());
        assert_eq!(added.details().name(), "Original Star System");
        assert_eq!(added.details().description(), "Original Description");
        Ok(())
    }
}
