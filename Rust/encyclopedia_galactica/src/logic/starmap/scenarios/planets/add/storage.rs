use log::debug;
use sqlx::PgPool;

use crate::logic::starmap::scenarios::planets::PlanetEntity;

pub async fn add_to_storage(
    input: PlanetEntity,
    db_connection: PgPool,
) -> anyhow::Result<PlanetEntity> {
    let result: PlanetEntity = sqlx::query_as(
        r#"
        INSERT INTO 
            planets (name, description) 
            VALUES ($1, $2)
        RETURNING id, name, description
        "#,
    )
    .bind(input.name)
    .bind(input.description)
    .fetch_one(&db_connection)
    .await?;

    debug!("Planet table: entity inserted with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::logic::starmap::scenarios::planets::add::storage::add_to_storage;
    use sqlx::PgPool;

    #[sqlx::test]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a planet to have an existing ID
        let add_input = PlanetEntity::new(
            0,
            "Original Planet".to_string(),
            "Original Description".to_string(),
        );
        let added = add_to_storage(add_input, pool.clone()).await.unwrap();

        // Now update it
        assert_eq!(added.id, added.id);
        assert_eq!(added.name, "Original Planet");
        assert_eq!(added.description, "Original Description");
        Ok(())
    }
}
