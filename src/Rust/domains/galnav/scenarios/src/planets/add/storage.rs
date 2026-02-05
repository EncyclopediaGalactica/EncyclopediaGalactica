use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::planets::PlanetEntity;

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
    use crate::planets::PlanetEntityDetails;

    use super::*;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_add_to_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./../../../migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add a planet
        let details = PlanetEntityDetails::new(
            "Original Planet".to_string(),
            "Original Description".to_string(),
        );
        let add_input = PlanetEntity::new(0, Json(details));
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        // Check
        assert!(added.id > 0);
        assert_eq!(added.details.name, "Original Planet");
        assert_eq!(added.details.description, "Original Description");
        Ok(())
    }
}
