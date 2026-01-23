use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::star_systems::StarSystemEntity;

pub async fn update_in_storage(
    input: StarSystemEntity,
    db_connection: PgPool,
) -> anyhow::Result<StarSystemEntity> {
    // First, check if the entity exists
    let _existing: (i64,) = sqlx::query_as("SELECT id FROM star_systems WHERE id = $1")
        .bind(input.id)
        .fetch_one(&db_connection)
        .await
        .with_context(|| format!("Failed to check if star system exists: (id: {})", input.id))?;

    let result: StarSystemEntity = sqlx::query_as(
        r#"
        UPDATE star_systems
        SET data = $2
        WHERE id = $1
        RETURNING id, data
        "#,
    )
    .bind(input.id)
    .bind(&input.data)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to update star system: (id: {})", input.id))?;

    debug!("Star system table: entity updated with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::star_systems::add::storage::add_to_storage;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a star system to have an existing ID
        let add_data = serde_json::json!({
            "name": "Original Star System",
            "description": "Original Description",
            "x": 0.0,
            "y": 0.0,
            "z": 0.0
        });
        let add_input = StarSystemEntity::new(0, add_data);
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        // Now update it
        let update_data = serde_json::json!({
            "name": "Updated Star System",
            "description": "Updated Description",
            "x": 1.0,
            "y": 2.0,
            "z": 3.0
        });
        let update_input = StarSystemEntity::new(added.id, update_data);
        let updated = update_in_storage(update_input, pool.clone()).await.unwrap();

        assert_eq!(updated.id, added.id);
        assert_eq!(updated.data["name"], "Updated Star System");
        assert_eq!(updated.data["description"], "Updated Description");
        Ok(())
    }
}
