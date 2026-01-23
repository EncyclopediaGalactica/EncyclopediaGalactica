use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::star_systems::StarSystemEntity;

pub async fn add_to_storage(
    input: StarSystemEntity,
    db_connection: PgPool,
) -> anyhow::Result<StarSystemEntity> {
    let result: StarSystemEntity = sqlx::query_as(
        r#"
        INSERT INTO
            star_systems (data)
            VALUES ($1)
        RETURNING id, data
        "#,
    )
    .bind(&input.data)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert star system: (data: {:?})", input.data))?;

    debug!(
        "Star system table: entity inserted with id: {:?}",
        result.id
    );
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::star_systems::add::storage::add_to_storage;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_add_to_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a star system
        let data = serde_json::json!({
            "name": "Original Star System",
            "description": "Original Description",
            "x": 0.0,
            "y": 0.0,
            "z": 0.0
        });
        let add_input = StarSystemEntity::new(0, data);
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        assert_eq!(added.id, added.id);
        assert_eq!(added.data["name"], "Original Star System");
        assert_eq!(added.data["description"], "Original Description");
        Ok(())
    }
}
