pub async fn get_book_id_by_reference(
    reference: &str,
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<i64> {
    let result = sqlx::query_as::<_, ChapterIdEntity>("SELECT id FROM books WHERE reference = $1")
        .bind(reference)
        .fetch_one(&db_connection)
        .await?;
    Ok(result.id)
}

// `ChapterIdEntity` is a partial struct of `ChapterEntity`.
// Its sole purpose is being used in the `get_chapter_id_by_reference` function to work with the id.
#[derive(sqlx::FromRow, Debug)]
pub struct ChapterIdEntity {
    pub id: i64,
}
