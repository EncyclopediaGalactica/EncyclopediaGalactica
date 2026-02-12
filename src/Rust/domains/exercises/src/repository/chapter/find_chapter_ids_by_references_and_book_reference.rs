use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

pub async fn find_chapter_ids_by_references_and_book_reference(
    chapter_references: Vec<String>,
    book_reference: String,
    pool: Pool<Postgres>,
) -> anyhow::Result<Vec<i64>> {
    debug!(
        "find_chapter_ids_by_references_and_book_reference: {:#?}, {:#?}",
        chapter_references, book_reference
    );
    match sqlx::query_scalar::<_, i64>(
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
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to find chapter ids by references and book reference: {:#?} at {}:{}",
            nope,
            file!(),
            line!()
        )),
    }
}
