use anyhow::{Context, Result};
use log::debug;
use sqlx::PgPool;

use super::types::DeletePlanetScenarioInput;

/// Deletes a planet from the database
pub async fn delete_from_storage(pool: &PgPool, input: DeletePlanetScenarioInput) -> Result<()> {
    let rows_affected = sqlx::query!(
        r#"
        DELETE 
        FROM planets 
        WHERE id = $1
        "#,
        input.id
    )
    .execute(pool)
    .await
    .with_context(|| format!("Failed to delete planet: (id: {})", input.id))?
    .rows_affected();

    debug!("Deleted {} planet(s) with id {}", rows_affected, input.id);

    Ok(())
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::planets::{PlanetEntity, PlanetEntityDetails};
use sqlx::types::Json;
    use crate::scenarios::planets::add::storage::add_to_storage;
    use crate::scenarios::planets::get_all::storage::get_all_from_storage;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // Insert a planet
        let details = PlanetEntityDetails::new("Test Planet".to_string(), "Test Description".to_string());
        let planet = PlanetEntity::new(0, Json(details));
        let added = add_to_storage(planet, pool.clone()).await.unwrap();

        // Delete it
        let delete_input = DeletePlanetScenarioInput { id: added.id };
        delete_from_storage(&pool, delete_input).await.unwrap();

        // Verify it's gone
        let remaining = get_all_from_storage(&pool).await.unwrap();
        let hit = remaining.iter().find(|p| p.id() == added.id);
        assert!(hit.is_none());
        Ok(())
    }
}
