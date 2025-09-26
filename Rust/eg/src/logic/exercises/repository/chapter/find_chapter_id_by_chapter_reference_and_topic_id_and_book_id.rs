use log::debug;

pub async fn find_chapter_id_by_chapter_reference_and_topic_id_and_book_id(
    reference: &str,
    topic_id: i64,
    book_id: i64,
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<i64> {
    debug!(
        "find_chapter_id_by_chapter_reference_and_topic_id_and_book_id: reference: {:#?}, topic_id: {:#?}, book_id: {:#?}",
        reference, topic_id, book_id
    );
    match sqlx::query_scalar::<_, i64>(
        "SELECT id FROM chapters WHERE reference = $1 AND topic_id = $2 AND book_id = $3",
    )
    .bind(reference)
    .bind(topic_id)
    .bind(book_id)
    .fetch_one(&db_connection)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to find chapter id by chapter reference and topic id and book id: {:#?} at {}:{}",
            nope,
            file!(),
            line!()
        )),
    }
}
