use anyhow::Context;
use log::debug;
use sqlx::PgPool;

use crate::scenarios::moons::MoonEntity;

pub async fn add_to_storage(
    input: MoonEntity,
    db_connection: PgPool,
) -> anyhow::Result<MoonEntity> {
    let result: MoonEntity = sqlx::query_as(
        r#"
        INSERT INTO 
            moons (name, description) 
            VALUES ($1, $2)
        RETURNING id, name, description
        "#,
    )
    .bind(&input.name)
    .bind(&input.description)
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to insert moon: (name: {:?}", input.name))?;

    debug!("Moon table: entity inserted with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_add_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // Add a moon to test
        let add_input = MoonEntity::new(
            0,
            "Test Moon".to_string(),
            "Test Description".to_string(),
        );
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        assert_eq!(added.id, added.id);
        assert_eq!(added.name, "Test Moon");
        assert_eq!(added.description, "Test Description");
        Ok(())
    }
}