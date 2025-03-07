use actix_web::web;
use log::debug;
use sqlx::SqlitePool;

use crate::backend::BackendError;

use super::DocumentType;

pub async fn get_by_id(
    pool: web::Data<SqlitePool>,
    id: i64,
) -> Result<DocumentType, BackendError> {
    match get_from_database(pool, id).await {
        Err(error) => Err(BackendError::new(error.to_string(), error.to_string())),
        Ok(result) => Ok(result),
    }
}

async fn get_from_database(
    pool: web::Data<SqlitePool>,
    id: i64,
) -> Result<DocumentType, sqlx::Error> {
    sqlx::query("BEGIN").execute(pool.get_ref()).await?;

    match sqlx::query_as!(
        DocumentType,
        "
        SELECT id, name 
        FROM document_type 
        WHERE id = ?
        ",
        id
    )
    .fetch_one(pool.get_ref())
    .await
    {
        Err(e) => {
            sqlx::query("ROLLBACK").execute(pool.get_ref()).await?;
            debug!("Error at document_type.get_from_database rollback. {}", e);
            Err(e)
        },
        Ok(r) => {
            sqlx::query("COMMIT").execute(pool.get_ref()).await?;
            debug!("Document_type.get_from_database commit.");
            Ok(r)
        },
    }
}
