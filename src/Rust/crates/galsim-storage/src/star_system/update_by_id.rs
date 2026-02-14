use anyhow::Context;
use galsim_objects::star_system::entities::star_system::StarSystemEntity;
use log::debug;
use sqlx::PgPool;

pub async fn update_by_id(
    input: StarSystemEntity,
    db_connection: PgPool,
) -> anyhow::Result<StarSystemEntity> {
    // First, check if the entity exists
    let _existing: (i64,) = sqlx::query_as("SELECT id FROM star_systems WHERE id = $1")
        .bind(input.id())
        .fetch_one(&db_connection)
        .await
        .with_context(|| {
            format!(
                "Failed to check if star system exists: (id: {})",
                input.id()
            )
        })?;

    let result: StarSystemEntity = sqlx::query_as(
        r#"
        UPDATE star_systems
        SET details = $2
        WHERE id = $1
        RETURNING id, details
        "#,
    )
    .bind(input.id())
    .bind(&input.details())
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to update star system: (id: {})", input.id()))?;

    debug!(
        "Star system table: entity updated with id: {:?}",
        result.id()
    );
    Ok(result)
}

#[cfg(test)]
mod tests {
    use crate::star_system::add::add_star_system;

    use super::*;
    use galsim_objects::star_system::entities::star_system_details::StarSystemEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add a star system to have an existing ID
        let add_data = StarSystemEntityDetails::new(
            "Original Star System".to_string(),
            "Original Description".to_string(),
            Some(0.0),
            Some(0.0),
            Some(0.0),
        );
        let add_input = StarSystemEntity::new(0, Json(add_data));
        let added = add_star_system(add_input, pool.clone()).await.unwrap();

        // Now update it
        let update_data = StarSystemEntityDetails::new(
            "Updated Star System".to_string(),
            "Updated Description".to_string(),
            Some(1.0),
            Some(2.0),
            Some(3.0),
        );
        let update_input = StarSystemEntity::new(added.id(), Json(update_data));
        let updated = update_by_id(update_input, pool.clone()).await.unwrap();

        assert_eq!(updated.id(), added.id());
        assert_eq!(updated.details().name(), "Updated Star System");
        assert_eq!(updated.details().description(), "Updated Description");
        Ok(())
    }
}
