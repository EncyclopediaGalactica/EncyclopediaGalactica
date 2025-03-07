use actix_web::web;
use sqlx::SqlitePool;

use crate::backend::BackendError;

use super::{add_types::AddDocumentTypeInput, DocumentType, ToDocumentType};

pub async fn add(
    pool: web::Data<SqlitePool>,
    input: AddDocumentTypeInput,
) -> Result<DocumentType, BackendError> {
    validate(&input).await?;
    match persist(pool, &input.to_document_type()).await {
        Err(error) => Err(BackendError::new(error.to_string(), error.to_string())),
        Ok(result) => Ok(result),
    }
}

async fn validate(document_type: &AddDocumentTypeInput) -> Result<(), BackendError> {
    let mut errors = vec![];

    if document_type.name.trim().len() <= 3 {
        errors.push(String::from(
            "Name trimmed length must be equal or longer than 3.",
        ));
    }

    if !errors.is_empty() {
        Err(BackendError::new(
            format!("Error at recording Document Type: {}", errors.join("; ")),
            format!("Error at recording Document Type: {}", errors.join("; ")),
        ))
    } else {
        Ok(())
    }
}

async fn persist(
    pool: web::Data<SqlitePool>,
    document_type: &DocumentType,
) -> Result<DocumentType, sqlx::Error> {
    sqlx::query("BEGIN").execute(pool.get_ref()).await?;

    match sqlx::query_as!(
        DocumentType,
        "
        INSERT INTO document_type (name)
        VALUES (?)
        RETURNING id, name
        ",
        document_type.name
    )
    .fetch_one(pool.get_ref())
    .await
    {
        Err(error) => {
            sqlx::query("ROLLBACK").execute(pool.get_ref()).await?;
            Err(error)
        },
        Ok(result) => {
            sqlx::query("COMMIT").execute(pool.get_ref()).await?;
            Ok(result)
        },
    }
}
