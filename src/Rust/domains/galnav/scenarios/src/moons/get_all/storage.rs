use anyhow::Context;
use log::debug;
use sqlx::PgPool;
use sqlx::types::Json;

use crate::moons::MoonEntity;
use crate::moons::MoonEntityDetails;

pub async fn get_all_from_storage(db_connection: PgPool) -> anyhow::Result<Vec<MoonEntity>> {
    let result: Vec<MoonEntity> = sqlx::query_as!(
        MoonEntity,
        r#"
        SELECT 
            id, 
            details as "details: Json<MoonEntityDetails>"
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
    use crate::moons::add::storage::add_to_storage;

    use super::*;
    use sqlx::PgPool;

    #[sqlx::test]
    async fn test_get_all_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // Add a few moons
        let data1 = MoonEntityDetails::new("Moon 1".to_string(), "Description 1".to_string());
        let moon1 = MoonEntity::new(0, Json(data1));
        let data2 = MoonEntityDetails::new("Moon 2".to_string(), "Description 2".to_string());
        let moon2 = MoonEntity::new(0, Json(data2));
        add_to_storage(moon1, pool.clone()).await.unwrap();
        add_to_storage(moon2, pool.clone()).await.unwrap();

        // Get all
        let all_moons = get_all_from_storage(pool.clone()).await.unwrap();
        assert!(all_moons.len() >= 2);
        Ok(())
    }
}
