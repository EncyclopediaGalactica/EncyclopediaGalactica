use sqlx::FromRow;

use crate::logic::exercises::parsers::books::Book;
pub mod add;
pub mod find_book_id_by_reference;
pub mod find_book_ids_by_references;
pub mod truncate;

#[derive(Debug, FromRow)]
pub struct BookEntity {
    pub id: i32,
    pub title: String,
    pub authors: String,
    pub page_start: i32,
    pub page_end: i32,
    pub reference: String,
    pub topic_id: i32,
}

impl From<Book> for BookEntity {
    fn from(book: Book) -> Self {
        BookEntity {
            id: 0,
            title: book.title().to_string(),
            authors: book.authors().to_string(),
            page_start: book.page_start(),
            page_end: book.page_end(),
            reference: book.reference().to_string(),
            topic_id: 0,
        }
    }
}
