use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

pub async fn find_topic_id_by_reference(
    reference: &str,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<i64> {
    debug!("Looking for topic id by reference: {:?}", reference);
    let result = sqlx::query_scalar::<_, i64>("SELECT id FROM topics WHERE reference = $1")
        .bind(reference)
        .fetch_one(&db_connection)
        .await?;
    Ok(result)
}
