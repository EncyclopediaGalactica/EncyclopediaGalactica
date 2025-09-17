use serde_json::json;
use sqlx::FromRow;
use sqlx::Pool;
use sqlx::Postgres;

pub async fn add(
    db_connection: Pool<Postgres>,
    input: AddVertexScenarioInput,
) -> anyhow::Result<()> {
    let data = json!({ "data": input.data.clone() });
    sqlx::query(
        r#"
            INSERT INTO vertices (data)
            VALUES ($1)
            "#,
    )
    .bind(data)
    .execute(&db_connection)
    .await?;
    Ok(())
}

#[derive(Debug, Clone)]
pub struct AddVertexScenarioInput {
    pub data: String,
}

#[derive(Debug, Clone, FromRow)]
pub struct AddVertexScenarioResult {
    pub data: serde_json::Value,
}
