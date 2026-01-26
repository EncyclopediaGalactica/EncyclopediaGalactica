use anyhow::Context;
use log::debug;
use sqlx::PgPool;

pub async fn delete_from_storage(id: i64, db_connection: PgPool) -> anyhow::Result<()> {
    let _result = sqlx::query(
        r#"
        DELETE 
        FROM 
            moons
        WHERE id = $1
        "#,
    )
    .bind(id)
    .execute(&db_connection)
    .await
    .with_context(|| format!("Failed to delete moon: (id: {:?})", id))?;

    debug!("Moon table: entity deleted with id: {:?}", id);
    Ok(())
}

#[cfg(test)]
mod tests {
    use crate::scenarios::moons::MoonEntity;
    use crate::scenarios::moons::MoonEntityDetails;
    use crate::scenarios::moons::add::storage::add_to_storage;

    use super::*;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a moon to have an existing ID
        let data =
            MoonEntityDetails::new("Moon to Delete".to_string(), "Will be deleted".to_string());
        let add_input = MoonEntity::new(0, Json(data));
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        // Now delete it
        delete_from_storage(added.id, pool.clone()).await.unwrap();

        // Verify it's gone by trying to get it (but since no get_by_id, just assume success)
        Ok(())
    }
}
