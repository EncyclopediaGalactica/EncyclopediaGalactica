use actix_web::web;
use sqlx::SqlitePool;

use crate::backend::BackendError;

use super::{delete_types::DeleteDocumentTypeInput, DocumentType, ToDocumentType};

pub async fn delete_document_type(
    pool: web::Data<SqlitePool>,
    input: DeleteDocumentTypeInput,
) -> Result<(), BackendError> {
    validate(&input).await?;
    match persist(pool, input.to_document_type()).await {
        Err(error) => Err(BackendError::new(error.to_string(), error.to_string())),
        Ok(_r) => Ok(()),
    }
}

async fn validate(input: &DeleteDocumentTypeInput) -> Result<(), BackendError> {
    let mut errors = vec![];

    if input.id == 0 {
        errors.push(String::from("Id must be greater or equal to 1."));
    }

    if !errors.is_empty() {
        Err(BackendError::new(
            format!("Error at deleting Document Type: {}", errors.join("; ")),
            format!("Error at deleting Document Type: {}", errors.join("; ")),
        ))
    } else {
        Ok(())
    }
}

async fn persist(
    pool: web::Data<SqlitePool>,
    input: DocumentType,
) -> Result<(), sqlx::Error> {
    sqlx::query("BEGIN").execute(pool.get_ref()).await?;

    match sqlx::query(
        "
        DELETE 
        FROM document_type 
        WHERE id = ?
        ",
    )
    .bind(input.id)
    .execute(pool.get_ref())
    .await
    {
        Err(error) => {
            sqlx::query("ROLLBACK").execute(pool.get_ref()).await?;
            Err(error)
        },
        Ok(_r) => {
            sqlx::query("COMMIT").execute(pool.get_ref()).await?;
            Ok(())
        },
    }
}
