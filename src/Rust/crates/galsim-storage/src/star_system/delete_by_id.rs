use anyhow::Context;
use log::debug;
use sqlx::PgPool;

pub async fn delete_star_system_by_id(id: i64, db_connection: PgPool) -> anyhow::Result<()> {
    let result = sqlx::query(
        r#"
    DELETE 
    FROM 
        star_systems 
    WHERE id = $1
    "#,
    )
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
    use crate::star_system::add::add_star_system;
    use crate::star_system::get_all::get_all_star_systems;

    use super::*;
    use galsim_objects::star_system::entities::star_system::StarSystemEntity;
    use galsim_objects::star_system::entities::star_system_details::StarSystemEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add a star system to have an existing ID
        let data = StarSystemEntityDetails::new(
            "Star System to Delete".to_string(),
            "Description".to_string(),
            Some(0.0),
            Some(0.0),
            Some(0.0),
        );
        let add_input = StarSystemEntity::new(0, Json(data));
        let added = add_star_system(add_input, pool.clone()).await.unwrap();

        // Now delete it
        delete_star_system_by_id(added.id(), pool.clone())
            .await
            .unwrap();

        // Verify deletion by trying to fetch all
        let remaining: Vec<StarSystemEntity> = get_all_star_systems(pool).await.unwrap();
        let hit = remaining.iter().find(|s| s.id() == added.id());
        assert!(hit.is_none());
        Ok(())
    }
}
