use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use crate::logic::starmap::scenarios::planets::PlanetEntity;

pub async fn add_to_storage(
    input: PlanetEntity,
    db_connection: Pool<Postgres>,
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
