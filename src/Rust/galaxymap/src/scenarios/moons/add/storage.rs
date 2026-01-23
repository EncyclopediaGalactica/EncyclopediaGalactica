use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::moons::MoonEntity;

pub async fn add_to_storage(
    input: MoonEntity,
    db_connection: PgPool,
) -> anyhow::Result<MoonEntity> {
    let result: MoonEntity = sqlx::query_as(
        r#"
        INSERT INTO
            moons (data)
            VALUES ($1)
        RETURNING id, data
        "#,
    )
    .bind(&input.data)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert moon: (data: {:?}", input.data))?;

    debug!("Moon table: entity inserted with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_add_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // Add a moon to test
        let data = serde_json::json!({
            "name": "Test Moon",
            "description": "Test Description"
        });
        let add_input = MoonEntity::new(0, data);
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        assert_eq!(added.id, added.id);
        assert_eq!(added.data["name"], "Test Moon");
        assert_eq!(added.data["description"], "Test Description");
        Ok(())
    }
}
