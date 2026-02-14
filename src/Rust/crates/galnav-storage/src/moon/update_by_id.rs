use anyhow::Context;
use galnav_objects::moon::entities::moon_entity::MoonEntity;
use log::debug;
use sqlx::PgPool;

pub async fn update_moon_by_id(
    input: MoonEntity,
    db_connection: PgPool,
) -> anyhow::Result<MoonEntity> {
    let result: MoonEntity = sqlx::query_as::<_, MoonEntity>(
        r#"
            UPDATE 
                moons
            SET 
                details = $1
            WHERE 
                id = $2
            RETURNING 
                id, 
                details
        "#,
    )
    .bind(input.details())
    .bind(input.id())
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to update moon: (id: {:?})", input.id()))?;

    debug!("Moon table: entity updated with id: {:?}", result.id());
    Ok(result)
}

#[cfg(test)]
mod tests {
    use crate::moon::add::add_moon;

    use super::*;
    use galnav_objects::moon::entities::moon_entity_details::MoonEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add a moon to have an existing ID
        let add_data = MoonEntityDetails::new(
            "Original Moon".to_string(),
            "Original Description".to_string(),
        );
        let add_input = MoonEntity::new(0, Json(add_data));
        let added = add_moon(add_input, pool.clone()).await.unwrap();

        // Now update it
        let update_data = MoonEntityDetails::new(
            "Updated Moon".to_string(),
            "Updated Description".to_string(),
        );
        let update_input = MoonEntity::new(added.id(), Json(update_data));
        let updated = update_moon_by_id(update_input, pool.clone()).await.unwrap();

        assert_eq!(updated.id(), added.id());
        assert_eq!(updated.details().name(), "Updated Moon");
        assert_eq!(updated.details().description(), "Updated Description");
        Ok(())
    }
}
