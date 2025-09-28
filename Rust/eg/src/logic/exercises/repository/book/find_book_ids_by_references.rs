use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

pub async fn find_book_ids_by_references(
    reference: Vec<String>,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<Vec<i64>> {
    debug!("find_book_ids_by_references: {:#?}", reference);
    match sqlx::query_scalar::<_, i64>(
        r#"
        SELECT 
            id 
        FROM 
            books 
        WHERE 
            reference = ANY($1)
        "#,
    )
    .bind(&reference)
    .fetch_all(&db_connection)
    .await
    {
        Ok(yolo) => {
            debug!("find_book_ids_by_references result: {:#?}", yolo);
            Ok(yolo)
        }
        Err(nope) => Err(anyhow::anyhow!(
            "{:#?} Failed to find book ids by references at {}:{} with input: {:#?}",
            nope,
            file!(),
            line!(),
            &reference,
        )),
    }
}
