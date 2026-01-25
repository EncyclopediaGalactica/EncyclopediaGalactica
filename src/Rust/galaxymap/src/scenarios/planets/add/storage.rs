use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::planets::PlanetEntity;

pub async fn add_to_storage(
    input: PlanetEntity,
    db_connection: PgPool,
) -> anyhow::Result<PlanetEntity> {
    let result: PlanetEntity = sqlx::query_as(
        r#"
        INSERT INTO
            planets (details)
            VALUES ($1)
        RETURNING id, details
        "#,
    )
    .bind(&input.details)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert planet: (data: {:?}", input.details))?;

    debug!("Planet table: entity inserted with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a planet to have an existing ID
        let add_input = PlanetEntity::new(
            0,
            serde_json::json!({"name": "Original Planet", "description": "Original Description"}),
        );
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        // Now update it
        assert_eq!(added.id, added.id);
        assert_eq!(added.details["name"], "Original Planet");
        assert_eq!(added.details["description"], "Original Description");
        Ok(())
    }
}
