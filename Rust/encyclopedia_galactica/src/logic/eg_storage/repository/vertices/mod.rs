use sqlx::FromRow;

pub mod add;

#[derive(Debug, Clone, FromRow)]
pub struct VertexEntity {
    pub id: i64,
    pub data: serde_json::Value,
}

impl VertexEntity {
    pub fn new(data: serde_json::Value) -> Self {
        Self { id: 0, data: data }
    }
}
