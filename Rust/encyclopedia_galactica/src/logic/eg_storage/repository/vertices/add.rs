use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use super::VertexEntity;

pub async fn add_vertex(
    vertex: VertexEntity,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<u64> {
    let result = sqlx::query(
        r#"
            INSERT INTO vertices (data)
            VALUES ($1)
            "#,
    )
    .bind(vertex.data)
    .execute(&db_connection)
    .await?;
    let affected_rows = result.rows_affected();
    debug!("affected rows: {:?}", affected_rows);
    Ok(affected_rows)
}
