use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use super::PlanetEntity;

pub async fn update(
    input: PlanetEntity,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<PlanetEntity> {
    let result: PlanetEntity = sqlx::query_as(
        r#"
        UPDATE
            planets
            SET
            name = $1,
            description = $2
        WHERE
            id = $3
        RETURNING id, name, description
        "#,
    )
    .bind(input.name)
    .bind(input.description)
    .bind(input.id)
    .fetch_one(&db_connection)
    .await?;

    debug!(
        "Planet table: entity with id: {:?} has been updated.",
        result.id
    );
    Ok(result)
}
