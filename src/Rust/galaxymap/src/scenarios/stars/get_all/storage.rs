use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::stars::StarEntity;

pub async fn get_all_from_storage(db_connection: PgPool) -> anyhow::Result<Vec<StarEntity>> {
    let result: Vec<StarEntity> = sqlx::query_as(
        r#"
        SELECT id, details
        FROM stars
        ORDER BY id
        "#,
    )
    .fetch_all(&db_connection)
    .await
    .with_context(|| "Failed to get all stars")?;

    debug!("Star table: retrieved {} entities", result.len());
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_get_all_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // Add a few stars
        let data1 = serde_json::json!({
            "name": "Star 1",
            "description": "Description 1"
        });
        let star1 = StarEntity::new(0, data1);
        let data2 = serde_json::json!({
            "name": "Star 2",
            "description": "Description 2"
        });
        let star2 = StarEntity::new(0, data2);
        crate::scenarios::stars::add::storage::add_to_storage(star1, pool.clone())
            .await
            .unwrap();
        crate::scenarios::stars::add::storage::add_to_storage(star2, pool.clone())
            .await
            .unwrap();

        // Get all
        let all_stars = get_all_from_storage(pool.clone()).await.unwrap();
        assert!(all_stars.len() >= 2);
        Ok(())
    }
}
