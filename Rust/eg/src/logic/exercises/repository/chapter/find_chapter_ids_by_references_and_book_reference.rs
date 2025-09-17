use sqlx::Pool;
use sqlx::Postgres;

pub async fn find_chapter_ids_by_references_and_book_reference(
    chapter_references: Vec<String>,
    book_reference: String,
    pool: Pool<Postgres>,
) -> anyhow::Result<Vec<i64>> {
    let chapter_ids = sqlx::query_as::<_, FindChapterIdsByReferencesAndBookReference>(
        r#"
        SELECT id
        FROM chapters
        WHERE reference = ANY($1)
        AND book_id = (
            SELECT id
            FROM books
            WHERE reference = $2
        )
        "#,
    )
    .bind(chapter_references)
    .bind(book_reference)
    .fetch_all(&pool)
    .await?;
    let chapter_ids_result: Vec<i64> = chapter_ids.iter().map(|c| c.id).collect();
    Ok(chapter_ids_result)
}

#[derive(sqlx::FromRow, Debug)]
struct FindChapterIdsByReferencesAndBookReference {
    pub id: i64,
}
