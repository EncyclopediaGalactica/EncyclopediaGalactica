use sqlx::Pool;
use sqlx::Postgres;

pub async fn find_book_ids_by_references(
    reference: Vec<String>,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<Vec<i64>> {
    let result = sqlx::query_scalar::<_, i64>("SELECT id FROM books WHERE reference = ANY($1)")
        .bind(reference)
        .fetch_all(&db_connection)
        .await?;
    Ok(result)
}
