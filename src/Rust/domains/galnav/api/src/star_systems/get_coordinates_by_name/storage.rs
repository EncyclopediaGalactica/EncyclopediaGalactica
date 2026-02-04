use anyhow::Context;
use sqlx::PgPool;

use crate::star_systems::StarSystemEntity;

pub async fn get_from_storage(
    star_system_name: &str,
    db_connection: PgPool,
) -> anyhow::Result<StarSystemEntity> {
    let result: StarSystemEntity = sqlx::query_as::<_, StarSystemEntity>(
        r#"
        SELECT
            id,
            details as "details: Json<StarSystemEntityDetails>"
        FROM 
            star_systems
        WHERE
            details->>'name' = $1
    "#,
    )
    .bind(star_system_name)
    .fetch_one(&db_connection)
    .await
    .with_context(|| {
        format!(
            "Failed to get star system by name: (name: {})",
            star_system_name
        )
    })?;

    Ok(result)
}
