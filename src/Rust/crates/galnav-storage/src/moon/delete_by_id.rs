use anyhow::Context;
use log::debug;
use sqlx::PgPool;

pub async fn delete_moon_by_id(id: i64, db_connection: PgPool) -> anyhow::Result<()> {
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
    use galnav_objects::moon::entities::moon_entity::MoonEntity;
    use galnav_objects::moon::entities::moon_entity_details::MoonEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    use crate::moon::add::add_moon;
    use crate::moon::delete_by_id::delete_moon_by_id;

    #[sqlx::test]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        let data =
            MoonEntityDetails::new("Moon to Delete".to_string(), "Will be deleted".to_string());
        let add_input = MoonEntity::new(0, Json(data));
        let added = add_moon(add_input, pool.clone()).await.unwrap();

        delete_moon_by_id(added.id(), pool.clone()).await.unwrap();

        Ok(())
    }
}
