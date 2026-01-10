use anyhow::{Context, Result};
use log::debug;
use sqlx::{PgPool, Row};

use super::types::UpdatePlanetScenarioInput;
use crate::logic::starmap::scenarios::planets::PlanetEntity;

/// Updates a planet in the database after verifying the ID exists
pub async fn update_in_storage(
    pool: &PgPool,
    input: UpdatePlanetScenarioInput,
) -> Result<PlanetEntity> {
    // First, check if the planet exists
    let exists: Option<i64> = sqlx::query("SELECT id FROM planets WHERE id = $1")
        .bind(input.id)
        .fetch_optional(pool)
        .await?
        .map(|row| row.get(0));

    if exists.is_none() {
        return Err(anyhow::anyhow!("Id does not exist"));
    }

    // Perform the update
    let row = sqlx::query(
        "UPDATE planets SET name = $2, description = $3 WHERE id = $1 RETURNING id, name, description",
    )
    .bind(input.id)
    .bind(&input.name)
    .bind(&input.description)
    .fetch_one(pool)
    .await
    .context("Failed to update planet")?;

    let entity = PlanetEntity::new(row.get(0), row.get(1), row.get(2));

    debug!("Updated planet: {:?}", entity);

    Ok(entity)
}

#[cfg(test)]
mod tests {

    // Note: For unit tests, we would need to mock the database.
    // Since this is an async function with database calls, integration tests will cover it.
    // Here we can test error handling if we mock, but for now, placeholder.

    #[test]
    fn test_placeholder() {
        // Unit tests for storage would require mocking PgPool
        // Integration tests in update.rs will test this
        assert!(true);
    }
}
