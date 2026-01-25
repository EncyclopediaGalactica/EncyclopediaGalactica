use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::moons::MoonEntity;

pub async fn get_all_from_storage(db_connection: PgPool) -> anyhow::Result<Vec<MoonEntity>> {
    let result: Vec<MoonEntity> = sqlx::query_as(
        r#"
        SELECT id, details
        FROM moons
        ORDER BY id
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
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_get_all_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // Add a few moons
        let data1 = serde_json::json!({
            "name": "Moon 1",
            "description": "Description 1"
        });
        let moon1 = crate::scenarios::moons::MoonEntity::new(0, data1);
        let data2 = serde_json::json!({
            "name": "Moon 2",
            "description": "Description 2"
        });
        let moon2 = crate::scenarios::moons::MoonEntity::new(0, data2);
        crate::scenarios::moons::add::storage::add_to_storage(moon1, pool.clone())
            .await
            .unwrap();
        crate::scenarios::moons::add::storage::add_to_storage(moon2, pool.clone())
            .await
            .unwrap();

        // Get all
        let all_moons = get_all_from_storage(pool.clone()).await.unwrap();
        assert!(all_moons.len() >= 2);
        Ok(())
    }
}
