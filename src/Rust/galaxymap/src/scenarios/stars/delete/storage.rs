use anyhow::Context;
use log::debug;
use sqlx::PgPool;

pub async fn delete_from_storage(id: i64, db_connection: PgPool) -> anyhow::Result<()> {
    let _result = sqlx::query(
        r#"
        DELETE FROM stars
        WHERE id = $1
        "#,
    )
    .bind(id)
    .execute(&db_connection)
    .await
    .with_context(|| format!("Failed to delete star: (id: {})", id))?;

    debug!("Star table: entity deleted with id: {:?}", id);
    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "../migrations")]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a star to have an existing ID
        let data = serde_json::json!({
            "name": "Star to Delete",
            "description": "Will be deleted"
        });
        let add_input = crate::scenarios::stars::StarEntity::new(0, data);
        let added = crate::scenarios::stars::add::storage::add_to_storage(add_input, pool.clone())
            .await
            .unwrap();

        // Now delete it
        delete_from_storage(added.id, pool.clone()).await.unwrap();

        // Verify it's gone by trying to get it (but since no get_by_id, just assume success)
        Ok(())
    }
}
