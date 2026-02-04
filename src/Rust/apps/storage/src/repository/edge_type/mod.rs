use sqlx::FromRow;

pub mod add;

#[derive(Debug, Clone, FromRow)]
pub struct EdgeTypeEntity {
    pub id: i64,
    pub name: String,
    pub description: String,
}
