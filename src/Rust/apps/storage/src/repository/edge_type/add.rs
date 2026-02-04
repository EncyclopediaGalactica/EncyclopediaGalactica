use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use super::EdgeTypeEntity;

pub async fn save_edge_type(
    input: EdgeTypeEntity,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<u64> {
    let result = sqlx::query(
        r#"
        INSERT INTO 
            edge_types (name, description) 
            VALUES ($1, $2)
        "#,
    )
    .bind(input.name)
    .bind(input.description)
    .execute(&db_connection)
    .await?;
    debug!("affected rows: {:?}", result.rows_affected());
    Ok(result.rows_affected())
}
