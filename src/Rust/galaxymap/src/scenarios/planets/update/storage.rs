use anyhow::{Context, Result};
use log::debug;
use sqlx::{PgPool, Row};

use super::types::UpdatePlanetScenarioInput;
use crate::scenarios::planets::PlanetEntity;

/// Updates a planet in the database after verifying the ID exists
pub async fn update_in_storage(
    pool: &PgPool,
    input: UpdatePlanetScenarioInput,
) -> Result<PlanetEntity> {
    // First, check if the planet exists
    let exists: Option<i64> = sqlx::query(
        r#"
    SELECT id 
    FROM planets 
    WHERE id = $1
    "#,
    )
    .bind(input.id)
    .fetch_optional(pool)
    .await?
    .map(|row| row.get(0));

    if exists.is_none() {
        return Err(anyhow::anyhow!("Id does not exist"));
    }

    // Perform the update
    let row = sqlx::query(
        r#"
        UPDATE planets 
        SET details = $2 
        WHERE id = $1 
        RETURNING id, details
        "#,
    )
    .bind(input.id)
    .bind(&input.details)
    .fetch_one(pool)
    .await
    .with_context(|| "Failed to update planet")?;

    let entity = PlanetEntity::new(row.get(0), row.get(1));

    debug!("Updated planet: {:?}", entity);

    Ok(entity)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::planets::add::storage::add_to_storage;
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
        let update_input = UpdatePlanetScenarioInput {
            id: added.id,
            details: serde_json::json!({"name": "Updated Planet", "description": "Updated Description"}),
        };
        let result = update_in_storage(&pool, update_input).await.unwrap();

        assert_eq!(result.id, added.id);
        assert_eq!(result.details["name"], "Updated Planet");
        assert_eq!(result.details["description"], "Updated Description");
        Ok(())
    }
}
