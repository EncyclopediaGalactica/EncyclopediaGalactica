use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use super::PlanetEntity;

pub async fn get_by_id(input: i64, db_connection: Pool<Postgres>) -> anyhow::Result<PlanetEntity> {
    let result: PlanetEntity = sqlx::query_as(
        r#"
        SELECT
            name
            , description
        FROM
            planets
        WHERE
            id = $1
        "#,
    )
    .bind(input)
    .fetch_one(&db_connection)
    .await?;

    debug!("Planet table: entity inserted with id: {:?}", result.id);
    Ok(result)
}
