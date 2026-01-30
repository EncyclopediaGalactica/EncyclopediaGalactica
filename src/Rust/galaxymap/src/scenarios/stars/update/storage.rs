use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::stars::StarEntity;

pub async fn update_in_storage(
    input: StarEntity,
    db_connection: PgPool,
) -> anyhow::Result<StarEntity> {
    let result: StarEntity = sqlx::query_as(
        r#"
        UPDATE stars
        SET details = $1
        WHERE id = $2
        RETURNING id, details
        "#,
    )
    .bind(&input.details)
    .bind(input.id)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to update star: (id: {})", input.id))?;

    debug!("Star table: entity updated with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        use sqlx::types::Json;

        use crate::scenarios::stars::StarEntityDetails;

        // First, add a star to have an existing ID
        let add_details = StarEntityDetails::new("Original Star".to_string(), "Original Description".to_string());
        let add_input = StarEntity::new(0, Json(add_details));
        let added = crate::scenarios::stars::add::storage::add_to_storage(add_input, pool.clone())
            .await
            .unwrap();

        // Now update it
        let update_details = StarEntityDetails::new("Updated Star".to_string(), "Updated Description".to_string());
        let update_input = StarEntity::new(added.id, Json(update_details));
        let updated = update_in_storage(update_input, pool.clone()).await.unwrap();

        assert_eq!(updated.id, added.id);
        assert_eq!(updated.details.name, "Updated Star");
        assert_eq!(updated.details.description, "Updated Description");
        Ok(())
    }
}
