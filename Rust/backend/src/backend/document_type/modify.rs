use actix_web::web;
use log::{debug, info};
use sqlx::SqlitePool;

use crate::backend::BackendError;

use super::{
    modify_types::ModifyDocumentTypeInput, DocumentType, DocumentTypeResult, ToDocumentType,
};

pub async fn modify(
    pool: web::Data<SqlitePool>,
    input: ModifyDocumentTypeInput,
) -> Result<DocumentTypeResult, BackendError> {
    debug!("document_type.modify input: {}", &input);
    validate(&input).await?;

    match persist(pool, &input.to_document_type()).await {
        Err(e) => Err(BackendError::new(e.to_string(), e.to_string())),
        Ok(res) => Ok(res.to_document_type_result()),
    }
}

async fn validate(document_type: &ModifyDocumentTypeInput) -> Result<(), BackendError> {
    let mut errors = vec![];

    if document_type.id < 1 {
        errors.push(String::from("Id must be greater or equal to 1."));
    }

    if document_type.name.trim().len() <= 3 {
        errors.push(String::from(
            "Name trimmed length must be equal or longer than 3.",
        ));
    }

    if !errors.is_empty() {
        Err(BackendError::new(
            format!("Error at modifying Document Type: {}", errors.join("; ")),
            format!("Error at modifying Document Type: {}", errors.join("; ")),
        ))
    } else {
        Ok(())
    }
}

async fn persist(
    pool: web::Data<SqlitePool>,
    document_type: &DocumentType,
) -> Result<DocumentType, sqlx::Error> {
    sqlx::query("BEGIN")
        .execute(pool.get_ref())
        .await
        .unwrap_or_else(|e| {
            debug!(
                "Error happened starting transaction for document_type.modify.persist. Error: {}",
                e
            );
            panic!(
                "Error happened starting transaction for document_type.modify.persist. Error: {}",
                e
            );
        });

    match sqlx::query_as!(
        DocumentType,
        "SELECT 
            id, name
        FROM
            document_type
        WHERE
            id = ?",
        document_type.id
    )
    .fetch_one(pool.get_ref())
    .await
    {
        Err(e) => {
            debug!("Error while checking if record exists for method: document_type.modify.persist. Error: {}", e);
            panic!("Error while checking if record exists for method: document_type.modify.persist. Error: {}", e);
        },
        Ok(r) => {
            info!("Returned record is: {}", r);
        },
    };

    match sqlx::query_as!(
        DocumentType,
        "
        UPDATE document_type
        SET name = ?
        WHERE id = ?
        RETURNING id, name
        ",
        document_type.name,
        document_type.id
    )
    .fetch_one(pool.get_ref())
    .await
    {
        Err(e) => {
            debug!(
                "Operation is rollbacked. Operation: document_type.modify.persist. Error: {}",
                e
            );
            sqlx::query("ROLLBACK").execute(pool.get_ref()).await?;
            Err(e)
        },
        Ok(res) => {
            sqlx::query("COMMIT").execute(pool.get_ref()).await?;
            Ok(res)
        },
    }
}
