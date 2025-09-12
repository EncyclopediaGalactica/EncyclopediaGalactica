use sqlx::Pool;
use sqlx::Postgres;

use super::TopicEntity;

pub async fn add_topic(
    topic_entity: TopicEntity,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    match sqlx::query("INSERT INTO topics (name, reference) VALUES ($1, $2)")
        .bind(topic_entity.name)
        .bind(topic_entity.reference)
        .execute(&db_connection)
        .await
    {
        Ok(yolo) => {
            let affected_rows = yolo.rows_affected();
            println!("topic affected rows: {:#?}", affected_rows);
            Ok(())
        }
        Err(nopes) => Err(anyhow::anyhow!("add_topic method: {:#?}", nopes)),
    }
}
