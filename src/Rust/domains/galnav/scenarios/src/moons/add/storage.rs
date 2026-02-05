use anyhow::Context;
use log::debug;
use serde_json::json;
use sqlx::PgPool;
use sqlx::types::Json;

use crate::moons::MoonEntity;
use crate::moons::MoonEntityDetails;

pub async fn add_to_storage(
    input: MoonEntity,
    db_connection: PgPool,
) -> anyhow::Result<MoonEntity> {
    let result: MoonEntity = sqlx::query_as!(
        MoonEntity,
        r#"
        INSERT INTO
            moons (details)
            VALUES ($1)
        RETURNING 
            id, 
            details as "details: Json<MoonEntityDetails>"
        "#,
        json!(input.details)
    )
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert moon: (data: {:?}", input.details))?;

    debug!("Moon table: entity inserted with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use sqlx::PgPool;
    use sqlx::types::Json;

    use crate::moons::MoonEntity;
    use crate::moons::MoonEntityDetails;

    use super::add_to_storage;

    #[sqlx::test]
    async fn test_add_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // Add a moon to test
        let data = MoonEntityDetails::new("Test Moon".to_string(), "Test Description".to_string());
        let add_input = MoonEntity::new(0, Json(data));
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        assert_eq!(added.id, added.id);
        assert_eq!(added.details.name, "Test Moon");
        assert_eq!(added.details.description, "Test Description");
        Ok(())
    }
}
