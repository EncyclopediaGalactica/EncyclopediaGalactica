use actix_web::web;
use sqlx::SqlitePool;

use crate::backend::BackendError;

use super::DocumentType;

pub async fn get_document_types(
    pool: web::Data<SqlitePool>
) -> Result<Vec<DocumentType>, BackendError> {
    match get_from_database(pool).await {
        Err(error) => Err(BackendError::new(error.to_string(), error.to_string())),
        Ok(result) => Ok(result),
    }
}

async fn get_from_database(pool: web::Data<SqlitePool>) -> Result<Vec<DocumentType>, sqlx::Error> {
    sqlx::query("BEGIN").execute(pool.get_ref()).await?;

    match sqlx::query_as!(
        DocumentType,
        "
        SELECT id, name 
        FROM document_type 
        ",
    )
    .fetch_all(pool.get_ref())
    .await
    {
        Err(e) => {
            sqlx::query("ROLLBACK").execute(pool.get_ref()).await?;
            Err(e)
        },
        Ok(r) => {
            sqlx::query("COMMIT").execute(pool.get_ref()).await?;
            Ok(r)
        },
    }
}
