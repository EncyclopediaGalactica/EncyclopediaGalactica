use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use crate::logic::exercises::repository::book::find_book_id_by_reference::find_book_id_by_reference;

use super::ChapterEntity;

pub async fn add_chapter(
    chapter_entity: ChapterEntity,
    book_reference: &str,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    debug!(
        "Adding chapter: chapter entity: {:#?}; book reference: {:#?}",
        chapter_entity, book_reference
    );
    let book_id = find_book_id_by_reference(book_reference, db_connection.clone()).await?;
    debug!("Found book id: {:#?}", book_id);
    match sqlx::query(
        "INSERT INTO chapters (title, reference, page_start, page_end, book_id) VALUES ($1, $2, $3, $4, $5)",
    )
    .bind(chapter_entity.title)
    .bind(chapter_entity.reference)
    .bind(chapter_entity.page_start)
    .bind(chapter_entity.page_end)
    .bind(book_id)
    .execute(&db_connection)
    .await {
        Ok(yolo) => {
            debug!("Added chapter with id: {:#?}", yolo.rows_affected());
            Ok(())
        },
        Err(nopes) => {
            Err(anyhow::anyhow!("Failed to add chapter: {:#?} at {}:{}", nopes, file!(), line!()))
        }
    }
}
