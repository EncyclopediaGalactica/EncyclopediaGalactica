use anyhow::Context;
use galsim_objects::moon::entities::moon_entity::MoonEntity;
use log::debug;
use sqlx::PgPool;

pub async fn get_all_moons(db_connection: PgPool) -> anyhow::Result<Vec<MoonEntity>> {
    let result: Vec<MoonEntity> = sqlx::query_as::<_, MoonEntity>(
        r#"
        SELECT 
            id, 
            details
        FROM 
            moons
        "#,
    )
    .fetch_all(&db_connection)
    .await
    .with_context(|| "Failed to get all moons")?;

    debug!("Moon table: retrieved {} entities", result.len());
    Ok(result)
}

#[cfg(test)]
mod tests {

    use crate::moon::add::add_moon;

    use super::*;
    use galsim_objects::moon::entities::moon_entity_details::MoonEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_get_all_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../galsim-migrations")
            .run(&pool)
            .await
            .unwrap();
        let data1 = MoonEntityDetails::new("Moon 1".to_string(), "Description 1".to_string());
        let moon1 = MoonEntity::new(0, Json(data1));
        let data2 = MoonEntityDetails::new("Moon 2".to_string(), "Description 2".to_string());
        let moon2 = MoonEntity::new(0, Json(data2));
        add_moon(moon1, pool.clone()).await.unwrap();
        add_moon(moon2, pool.clone()).await.unwrap();

        // Get all
        let all_moons = get_all_moons(pool.clone()).await.unwrap();
        assert!(all_moons.len() >= 2);
        Ok(())
    }
}
