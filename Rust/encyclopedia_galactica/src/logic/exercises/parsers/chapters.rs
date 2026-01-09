use std::fs::read_to_string;
use std::path::PathBuf;

use serde::Deserialize;
use serde::Serialize;
use toml::from_str;

pub fn parse_chapter_files(files: Vec<PathBuf>) -> anyhow::Result<Vec<Chapter>> {
    let mut chapters = Vec::new();
    if files.is_empty() {
        panic!("Expected list of books")
    } else {
        for path in files.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<Chapter>(&r) {
                Ok(parsed) => {
                    chapters.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}. Error: {}", &r, e)
                }
            }
        }
        Ok(chapters)
    }
}
#[derive(Clone, Debug, Serialize, Deserialize)]
pub struct Chapter {
    title: String,
    page_start: i32,
    page_end: i32,
    reference: String,
    book_reference: String,
}

impl Chapter {
    pub fn set_title(&mut self, title: String) {
        self.title = title;
    }

    pub fn title(&self) -> &str {
        &self.title
    }

    pub fn set_page_start(&mut self, page_start: i32) {
        self.page_start = page_start;
    }

    pub fn page_start(&self) -> i32 {
        self.page_start
    }

    pub fn set_page_end(&mut self, page_end: i32) {
        self.page_end = page_end;
    }

    pub fn page_end(&self) -> i32 {
        self.page_end
    }

    pub fn set_reference(&mut self, reference: String) {
        self.reference = reference;
    }

    pub fn reference(&self) -> &str {
        &self.reference
    }

    pub fn set_book_reference(&mut self, book_reference: String) {
        self.book_reference = book_reference;
    }

    pub fn book_reference(&self) -> &str {
        &self.book_reference
    }
}
