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
            star_systems (name, description, x, y, z)
            VALUES ($1, $2, $3, $4, $5)
        RETURNING id, name, description, x, y, z
        "#,
    )
    .bind(&input.name)
    .bind(&input.description)
    .bind(&input.x)
    .bind(&input.y)
    .bind(&input.z)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert star system: (name: {:?})", input.name))?;

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
        let add_input = StarSystemEntity::new(
            0,
            "Original Star System".to_string(),
            "Original Description".to_string(),
            Some(0.0),
            Some(0.0),
            Some(0.0),
        );
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        assert_eq!(added.id, added.id);
        assert_eq!(added.name, "Original Star System");
        assert_eq!(added.description, "Original Description");
        Ok(())
    }
}
