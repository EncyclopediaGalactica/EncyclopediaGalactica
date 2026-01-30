use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::stars::StarEntity;

pub async fn add_to_storage(
    input: StarEntity,
    db_connection: PgPool,
) -> anyhow::Result<StarEntity> {
    let result: StarEntity = sqlx::query_as(
        r#"
        INSERT INTO
            stars (details)
            VALUES ($1)
        RETURNING id, details
        "#,
    )
    .bind(&input.details)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert star: (data: {:?})", input.details))?;

    debug!("Star table: entity inserted with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_add_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        use sqlx::types::Json;

        use crate::scenarios::stars::StarEntityDetails;

        // Add a star to test
        let details = StarEntityDetails::new("Test Star".to_string(), "Test Description".to_string());
        let add_input = StarEntity::new(0, Json(details));
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        assert!(added.id > 0);
        assert_eq!(added.details.name, "Test Star");
        assert_eq!(added.details.description, "Test Description");
        Ok(())
    }
}
