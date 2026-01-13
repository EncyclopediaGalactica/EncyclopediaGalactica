use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::moons::MoonEntity;

pub async fn update_in_storage(
    input: MoonEntity,
    db_connection: PgPool,
) -> anyhow::Result<MoonEntity> {
    let result: MoonEntity = sqlx::query_as(
        r#"
        UPDATE moons
        SET name = $1, description = $2
        WHERE id = $3
        RETURNING id, name, description
        "#,
    )
    .bind(&input.name)
    .bind(&input.description)
    .bind(input.id)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to update moon: (id: {:?})", input.id))?;

    debug!("Moon table: entity updated with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a moon to have an existing ID
        let add_input = MoonEntity::new(
            0,
            "Original Moon".to_string(),
            "Original Description".to_string(),
        );
        let added = crate::scenarios::moons::add::storage::add_to_storage(add_input, pool.clone()).await.unwrap();

        // Now update it
        let update_input = MoonEntity::new(
            added.id,
            "Updated Moon".to_string(),
            "Updated Description".to_string(),
        );
        let updated = update_in_storage(update_input, pool.clone()).await.unwrap();

        assert_eq!(updated.id, added.id);
        assert_eq!(updated.name, "Updated Moon");
        assert_eq!(updated.description, "Updated Description");
        Ok(())
    }
}