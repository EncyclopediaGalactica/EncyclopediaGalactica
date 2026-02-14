use anyhow::{Context, Result};
use galsim_objects::planet::scenario_entities::delete_planet_scenario_input::DeletePlanetScenarioInput;
use log::debug;
use sqlx::PgPool;

pub async fn delete_by_id(pool: &PgPool, input: DeletePlanetScenarioInput) -> Result<()> {
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
    use crate::planet::add_planet::add_planet;
    use crate::planet::get_all::get_all_planets;

    use super::*;
    use galsim_objects::planet::entities::planet_entity::PlanetEntity;
    use galsim_objects::planet::entities::planet_entity_details::PlanetEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_delete_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./migrations")
            .run(&pool)
            .await
            .unwrap();
        // Insert a planet
        let details =
            PlanetEntityDetails::new("Test Planet".to_string(), "Test Description".to_string());
        let planet = PlanetEntity::new(0, Json(details));
        let added = add_planet(planet, pool.clone()).await.unwrap();

        // Delete it
        let delete_input = DeletePlanetScenarioInput { id: added.id() };
        delete_by_id(&pool, delete_input).await.unwrap();

        // Verify it's gone
        let remaining = get_all_planets(&pool).await.unwrap();
        let hit = remaining.iter().find(|p| p.id() == added.id());
        assert!(hit.is_none());
        Ok(())
    }
}
