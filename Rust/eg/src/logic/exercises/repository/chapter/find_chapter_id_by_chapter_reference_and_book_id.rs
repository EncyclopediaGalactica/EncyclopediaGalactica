pub async fn find_chapter_id_by_chapter_reference_and_book_id(
    reference: &str,
    book_id: i64,
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<i64> {
    let result = sqlx::query_scalar::<_, i64>(
        "SELECT id FROM chapters WHERE reference = $1 AND book_id = $2",
    )
    .bind(reference)
    .bind(book_id)
    .fetch_one(&db_connection)
    .await?;
    Ok(result)
}
