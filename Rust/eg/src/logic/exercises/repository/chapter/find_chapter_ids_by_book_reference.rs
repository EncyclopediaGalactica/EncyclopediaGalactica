pub async fn find_chapter_ids_by_book_reference(
    book_reference: String,
    pool: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<Vec<i64>> {
    let chapter_ids = sqlx::query_as::<_, FindChapterIdsByBookReference>(
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
    .await?;
    let chapter_ids_result: Vec<i64> = chapter_ids.iter().map(|c| c.id).collect();
    Ok(chapter_ids_result)
}

#[derive(sqlx::FromRow, Debug)]
struct FindChapterIdsByBookReference {
    pub id: i64,
}
