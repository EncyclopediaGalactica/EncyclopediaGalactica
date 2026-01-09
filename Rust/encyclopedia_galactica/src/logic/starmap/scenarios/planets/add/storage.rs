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
