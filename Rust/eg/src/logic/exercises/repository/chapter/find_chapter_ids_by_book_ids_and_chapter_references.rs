use log::debug;

pub async fn find_chapter_ids_by_book_ids_and_chapter_references(
    chapter_references: Vec<String>,
    book_ids: &Vec<i64>,
    pool: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<Vec<i64>> {
    debug!(
        "find_chapter_ids_by_book_ids_and_chapter_references: chapter references: {:#?}, book ids: {:#?}",
        chapter_references, book_ids
    );
    match sqlx::query_scalar::<_, i64>(
        r#"
        SELECT
            id
        FROM
            chapters
        WHERE 
            book_id IN ($1)
            AND reference IN ($2)
        "#,
    )
    .bind(book_ids)
    .bind(&chapter_references)
    .fetch_all(&pool)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "{:#?} Failed to find chapter id by book ids at {}:{} with input: {:#?}",
            nope,
            file!(),
            line!(),
            &chapter_references,
        )),
    }
}
