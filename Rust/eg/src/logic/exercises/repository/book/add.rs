use sqlx::Pool;
use sqlx::Postgres;

use crate::logic::exercises::repository::topic::get_topic_id_by_reference::get_topic_id_by_reference;

use super::BookEntity;

pub async fn add_book(
    book_entity: BookEntity,
    topic_reference: &str,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    let topic_id = get_topic_id_by_reference(topic_reference, db_connection.clone()).await?;
    match sqlx::query("INSERT INTO books (title, authors, page_start, page_end, reference, topic_id) VALUES ($1, $2, $3, $4, $5, $6)")
        .bind(book_entity.title)
        .bind(book_entity.authors)
        .bind(book_entity.page_start)
        .bind(book_entity.page_end)
        .bind(book_entity.reference)
        .bind(topic_id)
        .execute(&db_connection)
        .await
    {
        Ok(yolo) => {
            let affected_rows = yolo.rows_affected();
            println!("add_book affected rows: {:#?}", affected_rows);
            Ok(())
        }
        Err(nopes) => Err(anyhow::anyhow!("add_book nopes: {:#?}", nopes)),
    }
}
