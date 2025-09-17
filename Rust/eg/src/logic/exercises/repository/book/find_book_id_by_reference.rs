use sqlx::Pool;
use sqlx::Postgres;

pub async fn find_book_id_by_reference(
    reference: &str,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<i64> {
    let result = sqlx::query_as::<_, FindBookIdByReferenceEntity>(
        "SELECT id FROM books WHERE reference = $1",
    )
    .bind(reference)
    .fetch_one(&db_connection)
    .await?;
    Ok(result.id)
}

// `ChapterIdEntity` is a partial struct of `ChapterEntity`.
// Its sole purpose is being used in the `get_chapter_id_by_reference` function to work with the id.
#[derive(sqlx::FromRow, Debug)]
struct FindBookIdByReferenceEntity {
    pub id: i64,
}
