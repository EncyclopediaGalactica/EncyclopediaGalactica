use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::stars::StarEntity;

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
    use crate::stars::StarEntityDetails;
    use crate::stars::add::storage::add_to_storage;

    use super::*;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_get_all_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // Add a few stars
        let details1 = StarEntityDetails::new("Star 1".to_string(), "Description 1".to_string());
        let star1 = StarEntity::new(0, Json(details1));
        let details2 = StarEntityDetails::new("Star 2".to_string(), "Description 2".to_string());
        let star2 = StarEntity::new(0, Json(details2));
        add_to_storage(star1, pool.clone()).await.unwrap();
        add_to_storage(star2, pool.clone()).await.unwrap();

        // Get all
        let all_stars = get_all_from_storage(pool.clone()).await.unwrap();
        assert!(all_stars.len() >= 2);
        Ok(())
    }
}
