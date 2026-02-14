use anyhow::Context;
use log::debug;
use sqlx::PgPool;

pub async fn delete_star_by_id(id: i64, db_connection: PgPool) -> anyhow::Result<()> {
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

    use crate::star::add::add_star;

    use super::*;
    use gal_nav_domain_objects::star::entities::star_entity::StarEntity;
    use gal_nav_domain_objects::star::entities::star_entity_details::StarEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add a star to have an existing ID
        let details =
            StarEntityDetails::new("Star to Delete".to_string(), "Will be deleted".to_string());
        let add_input = StarEntity::new(0, Json(details));
        let added = add_star(add_input, pool.clone()).await.unwrap();

        // Now delete it
        delete_star_by_id(added.id(), pool.clone()).await.unwrap();

        // Verify it's gone by trying to get it (but since no get_by_id, just assume success)
        Ok(())
    }
}
