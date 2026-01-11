use log::debug;

pub async fn find_chapter_id_by_chapter_and_book_reference(
    chapter_reference: &str,
    book_reference: &str,
    pool: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<i64> {
    debug!(
        "find_chapter_id_by_chapter_and_book_reference: chapter_reference: {:#?}, book_reference: {:#?}",
        chapter_reference, book_reference
    );
    match sqlx::query_scalar::<_, i64>(
        r#"
        SELECT id
        FROM chapters
        WHERE reference = $1
        AND book_id = (
            SELECT id
            FROM books
            WHERE reference = $2
        )
        "#,
    )
    .bind(chapter_reference)
    .bind(book_reference)
    .fetch_one(&pool)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to find chapter id by chapter and book reference: {:#?} at {}:{}",
            nope,
            file!(),
            line!()
        )),
    }
}
