use log::debug;

pub async fn find_chapter_ids_by_book_ids(
    book_ids: &Vec<i64>,
    pool: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<Vec<i64>> {
    debug!("find_chapter_id_by_book_ids: {:#?}", &book_ids);
    match sqlx::query_scalar::<_, i64>(
        r#"
        SELECT
            id
        FROM
            chapters
        WHERE 
            book_id = ANY($1)
        "#,
    )
    .bind(&book_ids)
    .fetch_all(&pool)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "{:#?} Failed to find chapter id by book ids at {}:{} with input: {:#?}",
            nope,
            file!(),
            line!(),
            book_ids,
        )),
    }
}
