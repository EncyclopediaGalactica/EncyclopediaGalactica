use sqlx::FromRow;

use crate::logic::exercises::parsers::chapters::Chapter;
pub mod add;
pub mod find_chapter_id_by_chapter_and_book_reference;
pub mod find_chapter_ids_by_book_reference;
pub mod find_chapter_ids_by_references_and_book_reference;
pub mod truncate;

#[derive(Debug, FromRow)]
pub struct ChapterEntity {
    pub id: i64,
    pub title: String,
    pub reference: String,
    pub page_start: i32,
    pub page_end: i32,
    pub book_id: i64,
}

impl From<Chapter> for ChapterEntity {
    fn from(chapter: Chapter) -> Self {
        ChapterEntity {
            id: 0,
            title: chapter.title().to_string(),
            reference: chapter.reference().to_string(),
            page_start: chapter.page_start(),
            page_end: chapter.page_end(),
            book_id: 0,
        }
    }
}
