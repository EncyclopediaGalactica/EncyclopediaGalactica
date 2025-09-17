use sqlx::Pool;
use sqlx::Postgres;

pub async fn get_topic_id_by_reference(
    reference: &str,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<i64> {
    let result = sqlx::query_as::<_, TopicIdEntity>("SELECT id FROM topics WHERE reference = $1")
        .bind(reference)
        .fetch_one(&db_connection)
        .await?;
    Ok(result.id)
}

// `TopicIdEntity` is a partial struct of `TopicEntity`.
// Its sole purpose is being used in the `get_topic_id_by_reference` function to work with the id.
#[derive(sqlx::FromRow, Debug)]
pub struct TopicIdEntity {
    pub id: i64,
}
