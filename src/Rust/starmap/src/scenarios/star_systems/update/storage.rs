use log::debug;
use sqlx::PgPool;

use crate::scenarios::star_systems::StarSystemEntity;

pub async fn update_in_storage(
    input: StarSystemEntity,
    db_connection: PgPool,
) -> anyhow::Result<StarSystemEntity> {
    // First, check if the entity exists
    let _existing: (i64,) = sqlx::query_as("SELECT id FROM star_systems WHERE id = $1")
        .bind(input.id)
        .fetch_one(&db_connection)
        .await?;

    let result: StarSystemEntity = sqlx::query_as(
        r#"
        UPDATE star_systems 
        SET name = $2, description = $3 
        WHERE id = $1 
        RETURNING id, name, description
        "#,
    )
    .bind(input.id)
    .bind(input.name)
    .bind(input.description)
    .fetch_one(&db_connection)
    .await?;

    debug!("Star system table: entity updated with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::star_systems::add::storage::add_to_storage;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a star system to have an existing ID
        let add_input = StarSystemEntity::new(
            0,
            "Original Star System".to_string(),
            "Original Description".to_string(),
        );
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        // Now update it
        let update_input = StarSystemEntity::new(
            added.id,
            "Updated Star System".to_string(),
            "Updated Description".to_string(),
        );
        let updated = update_in_storage(update_input, pool.clone()).await.unwrap();

        assert_eq!(updated.id, added.id);
        assert_eq!(updated.name, "Updated Star System");
        assert_eq!(updated.description, "Updated Description");
        Ok(())
    }
}
