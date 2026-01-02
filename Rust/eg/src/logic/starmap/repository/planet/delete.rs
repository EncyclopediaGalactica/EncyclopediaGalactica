use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use super::PlanetEntity;

pub async fn delete(input: PlanetEntity, db_connection: Pool<Postgres>) -> anyhow::Result<()> {
    let result: PlanetEntity = sqlx::query_as(
        r#"
        DELETE
            planets
            WHERE id = $1
        "#,
    )
    .bind(input.id)
    .fetch_one(&db_connection)
    .await?;

    debug!(
        "Planet table: entity with id: {:?} has been deleted.",
        result.id
    );
    Ok(())
}
