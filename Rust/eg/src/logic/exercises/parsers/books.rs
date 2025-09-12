use std::fs::read_to_string;
use std::path::PathBuf;

use serde::Deserialize;
use serde::Serialize;
use toml::from_str;

pub fn parse_book_files(files: Vec<PathBuf>) -> anyhow::Result<Vec<Book>> {
    if files.is_empty() {
        panic!("Expected list of books")
    } else {
        let mut books = Vec::new();
        for path in files.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<Book>(&r) {
                Ok(parsed) => {
                    books.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}. Error: {}", &r, e)
                }
            }
        }
        Ok(books)
    }
}
#[derive(Clone, Debug, Serialize, Deserialize)]
pub struct Book {
    title: String,
    authors: String,
    page_start: i32,
    page_end: i32,
    reference: String,
    topic_reference: String,
}

impl Book {
    pub fn title(&self) -> &str {
        &self.title
    }

    pub fn set_title(&mut self, title: String) {
        self.title = title;
    }

    pub fn authors(&self) -> &str {
        &self.authors
    }

    pub fn set_authors(&mut self, authors: String) {
        self.authors = authors;
    }

    pub fn page_start(&self) -> i32 {
        self.page_start
    }

    pub fn set_page_start(&mut self, page_start: i32) {
        self.page_start = page_start;
    }

    pub fn page_end(&self) -> i32 {
        self.page_end
    }

    pub fn set_page_end(&mut self, page_end: i32) {
        self.page_end = page_end;
    }

    pub fn reference(&self) -> &str {
        &self.reference
    }

    pub fn set_reference(&mut self, reference: String) {
        self.reference = reference;
    }

    pub fn set_topic_reference(&mut self, topic_reference: String) {
        self.topic_reference = topic_reference;
    }

    pub fn topic_reference(&self) -> &str {
        &self.topic_reference
    }
}
