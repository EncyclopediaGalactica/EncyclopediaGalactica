use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::moons::MoonEntity;

pub async fn get_all_from_storage(db_connection: PgPool) -> anyhow::Result<Vec<MoonEntity>> {
    let result: Vec<MoonEntity> = sqlx::query_as(
        r#"
        SELECT id, name, description
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
        let moon1 = crate::scenarios::moons::MoonEntity::new(
            0,
            "Moon 1".to_string(),
            "Description 1".to_string(),
        );
        let moon2 = crate::scenarios::moons::MoonEntity::new(
            0,
            "Moon 2".to_string(),
            "Description 2".to_string(),
        );
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
