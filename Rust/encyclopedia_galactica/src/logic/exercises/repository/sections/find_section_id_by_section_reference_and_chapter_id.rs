use log::debug;

pub async fn find_section_id_by_section_reference_and_chapter_id(
    reference: &str,
    chapter_id: i64,
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<i64> {
    debug!(
        "find_section_id_by_section_reference_and_chapter_id: reference: {:#?}, chapter_id: {:#?}",
        reference, chapter_id
    );
    match sqlx::query_scalar::<_, i64>(
        r#"
        SELECT 
            id 
        FROM 
            sections 
        WHERE 
            reference = $1 
            AND chapter_id = $2
        "#,
    )
    .bind(reference)
    .bind(chapter_id)
    .fetch_one(&db_connection)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "{:#?}: Failed to find section id by section reference and chapter id at {}:{}. Input: reference: {:#?}, chapter_id: {:#?}",
            nope,
            file!(),
            line!(),
            reference,
            chapter_id
        )),
    }
}
