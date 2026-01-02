use log::debug;

pub async fn find_chapter_ids_by_book_reference(
    book_reference: String,
    pool: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<Vec<i64>> {
    debug!("find_chapter_ids_by_book_reference: {:#?}", book_reference);
    match sqlx::query_scalar::<_, i64>(
        r#"
        SELECT id
        FROM chapters
        WHERE book_id = (
            SELECT id
            FROM books
            WHERE reference = $1
        )
        "#,
    )
    .bind(book_reference)
    .fetch_all(&pool)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to find chapter ids by book reference: {:#?} at {}:{}",
            nope,
            file!(),
            line!()
        )),
    }
}
