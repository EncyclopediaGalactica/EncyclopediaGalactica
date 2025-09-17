use std::fs::read_to_string;
use std::path::PathBuf;

use serde::Deserialize;
use serde::Serialize;
use toml::from_str;

pub fn parse_section_files(files: Vec<PathBuf>) -> anyhow::Result<Vec<Section>> {
    let mut sections: Vec<Section> = Vec::new();
    if files.is_empty() {
        panic!("Expected list of books")
    } else {
        for file in files.into_iter() {
            let r: String = read_to_string::<_>(file.clone())?;
            match from_str::<Section>(&r) {
                Ok(parsed) => {
                    sections.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}, Error: {}", &r, e)
                }
            }
        }
        Ok(sections)
    }
}

#[derive(Clone, Debug, Serialize, Deserialize)]
pub struct Section {
    pub title: String,
    pub section_number: f64,
    pub page_start: i32,
    pub page_exercises_start: i32,
    pub concepts_questions_interval_start: String,
    pub concepts_questions_interval_end: String,
    pub skills_questions_interval_start: String,
    pub skills_questions_interval_end: String,
    pub applications_questions_interval_start: String,
    pub applications_questions_interval_end: String,
    pub discussion_questions_interval_start: String,
    pub discussion_questions_interval_end: String,
    pub page_end: i32,
    pub chapter_reference: String,
    pub book_reference: String,
}

impl Section {
    pub fn title(&self) -> &str {
        &self.title
    }

    pub fn set_title(&mut self, title: String) {
        self.title = title;
    }

    pub fn section_number(&self) -> f64 {
        self.section_number
    }

    pub fn set_section_number(&mut self, section_number: f64) {
        self.section_number = section_number;
    }

    pub fn set_page_start(&mut self, page_start: i32) {
        self.page_start = page_start;
    }

    pub fn page_start(&self) -> i32 {
        self.page_start
    }

    pub fn set_page_exercises_start(&mut self, page_exercises_start: i32) {
        self.page_exercises_start = page_exercises_start;
    }

    pub fn page_exercises_start(&self) -> i32 {
        self.page_exercises_start
    }

    pub fn set_concepts_questions_interval_start(
        &mut self,
        concepts_questions_interval_start: String,
    ) {
        self.concepts_questions_interval_start = concepts_questions_interval_start;
    }

    pub fn concepts_questions_interval_start(&self) -> &str {
        &self.concepts_questions_interval_start
    }

    pub fn concepts_questions_interval_end(&self) -> &str {
        &self.concepts_questions_interval_end
    }

    pub fn set_concepts_questions_interval_end(&mut self, concepts_questions_interval_end: String) {
        self.concepts_questions_interval_end = concepts_questions_interval_end;
    }

    pub fn set_skills_questions_interval_start(&mut self, skills_questions_interval_start: String) {
        self.skills_questions_interval_start = skills_questions_interval_start;
    }

    pub fn skills_questions_interval_start(&self) -> &str {
        &self.skills_questions_interval_start
    }

    pub fn set_skills_questions_interval_end(&mut self, skills_questions_interval_end: String) {
        self.skills_questions_interval_end = skills_questions_interval_end;
    }

    pub fn skills_questions_interval_end(&self) -> &str {
        &self.skills_questions_interval_end
    }

    pub fn set_applications_questions_interval_start(
        &mut self,
        applications_questions_interval_start: String,
    ) {
        self.applications_questions_interval_start = applications_questions_interval_start;
    }

    pub fn applications_questions_interval_start(&self) -> &str {
        &self.applications_questions_interval_start
    }

    pub fn set_applications_questions_interval_end(
        &mut self,
        applications_questions_interval_end: String,
    ) {
        self.applications_questions_interval_end = applications_questions_interval_end;
    }

    pub fn applications_questions_interval_end(&self) -> &str {
        &self.applications_questions_interval_end
    }

    pub fn set_discussion_questions_interval_start(
        &mut self,
        discussion_questions_interval_start: String,
    ) {
        self.discussion_questions_interval_start = discussion_questions_interval_start;
    }

    pub fn discussion_questions_interval_start(&self) -> &str {
        &self.discussion_questions_interval_start
    }

    pub fn set_discussion_questions_interval_end(
        &mut self,
        discussion_questions_interval_end: String,
    ) {
        self.discussion_questions_interval_end = discussion_questions_interval_end;
    }

    pub fn discussion_questions_interval_end(&self) -> &str {
        &self.discussion_questions_interval_end
    }

    pub fn set_page_end(&mut self, page_end: i32) {
        self.page_end = page_end;
    }

    pub fn page_end(&self) -> i32 {
        self.page_end
    }

    pub fn set_chapter_reference(&mut self, chapter_reference: String) {
        self.chapter_reference = chapter_reference;
    }

    pub fn chapter_reference(&self) -> &str {
        &self.chapter_reference
    }

    pub fn set_book_reference(&mut self, book_reference: String) {
        self.book_reference = book_reference;
    }

    pub fn book_reference(&self) -> &str {
        &self.book_reference
    }
}
