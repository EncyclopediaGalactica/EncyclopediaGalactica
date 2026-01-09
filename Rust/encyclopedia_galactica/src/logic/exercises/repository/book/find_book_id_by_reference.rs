use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

pub async fn find_book_id_by_reference(
    reference: &str,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<i64> {
    debug!("find_book_id_by_reference: reference: {:#?}", reference);
    match sqlx::query_scalar::<_, i64>(
        r#"
        SELECT 
            id 
        FROM 
            books 
        WHERE reference = $1
        "#,
    )
    .bind(reference)
    .fetch_one(&db_connection)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nopes) => Err(anyhow::anyhow!(
            "{:#?}: Failed to find book id by reference: reference: {:#?} at {}:{};",
            nopes,
            &reference,
            file!(),
            line!()
        )),
    }
}
