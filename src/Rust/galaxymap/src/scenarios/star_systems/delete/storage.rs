use anyhow::Context;
use log::debug;
use sqlx::PgPool;

pub async fn delete_from_storage(id: i64, db_connection: PgPool) -> anyhow::Result<()> {
    let result = sqlx::query("DELETE FROM star_systems WHERE id = $1")
        .bind(id)
        .execute(&db_connection)
        .await
        .with_context(|| format!("Failed to delete star system: (id: {})", id))?;

    debug!(
        "Star system table: deleted {} rows with id: {}",
        result.rows_affected(),
        id
    );
    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::star_systems::StarSystemEntity;
    use crate::scenarios::star_systems::add::storage::add_to_storage;
    use crate::scenarios::star_systems::get_all::storage::get_all_from_storage;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a star system to have an existing ID
        let data = serde_json::json!({
            "name": "Star System to Delete",
            "description": "Description",
            "x": 0.0,
            "y": 0.0,
            "z": 0.0
        });
        let add_input = StarSystemEntity::new(0, data);
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        // Now delete it
        delete_from_storage(added.id, pool.clone()).await.unwrap();

        // Verify deletion by trying to fetch all
        let remaining: Vec<StarSystemEntity> = get_all_from_storage(pool).await.unwrap();

        assert_eq!(remaining.len(), 0);
        Ok(())
    }
}
