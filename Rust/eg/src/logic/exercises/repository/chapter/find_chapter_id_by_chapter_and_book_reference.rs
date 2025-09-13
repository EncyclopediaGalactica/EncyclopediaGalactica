pub async fn find_chapter_id_by_chapter_and_book_reference(
    chapter_reference: &str,
    book_reference: &str,
    pool: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<i64> {
    let chapter_id = sqlx::query!(
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
        chapter_reference,
        book_reference
    )
    .fetch_one(&pool)
    .await?
    .id;
    Ok(chapter_id)
}
