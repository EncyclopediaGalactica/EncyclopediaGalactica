use sqlx::Pool;
use sqlx::Postgres;

use crate::logic::exercises::repository::topic::TopicEntity;

pub async fn get_topic_id_by_reference(
    reference: &str,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<i64> {
    let result = sqlx::query_as::<_, TopicEntity>("SELECT id FROM topics WHERE reference = $1")
        .bind(reference)
        .fetch_one(&db_connection)
        .await?;
    Ok(result.id)
}
