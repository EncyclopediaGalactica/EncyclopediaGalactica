use std::fs::File;
use std::io::BufRead;
use std::path::PathBuf;

use log::debug;
use regex::Regex;
use serde::Deserialize;
use serde::Serialize;

pub fn parse_added_exercise_files(files: Vec<PathBuf>) -> anyhow::Result<Vec<AddedExercise>> {
    let topic_reference = format!(
        r#"% \s*{}\s*=\s*(?:"([^"]*)"|[^\s]*)"#,
        regex::escape("topic_reference")
    );
    let topic_reference_pattern = Regex::new(&topic_reference).unwrap();

    let book_reference = format!(
        r#"% \s*{}\s*=\s*(?:"([^"]*)"|[^\s]*)"#,
        regex::escape("book_reference")
    );
    let book_reference_pattern = Regex::new(&book_reference).unwrap();

    let chapter_reference = format!(
        r#"% \s*{}\s*=\s*(?:"([^"]*)"|[^\s]*)"#,
        regex::escape("chapter_reference")
    );
    let chapter_reference_pattern = Regex::new(&chapter_reference).unwrap();

    let section_reference = format!(
        r#"% \s*{}\s*=\s*(?:"([^"]*)"|[^\s]*)"#,
        regex::escape("section_reference")
    );
    let section_reference_pattern = Regex::new(&section_reference).unwrap();

    let mut added_exercises: Vec<AddedExercise> = Vec::new();

    for added_exercise in files {
        let parsed_file = File::open(added_exercise.clone())?;
        debug!("Parsing added exercise file: {:?}", added_exercise.clone());
        let reader = std::io::BufReader::new(parsed_file);
        let mut added_exercise_result = AddedExercise::new();

        for a_single_line in reader.lines() {
            let line = a_single_line.unwrap();
            if let Some(topic_captures) = topic_reference_pattern.captures(&line) {
                if let Some(topic_captures_value) =
                    topic_captures.get(1).map(|m| m.as_str().trim().to_string())
                {
                    added_exercise_result.set_topic_reference(topic_captures_value.clone());
                    debug!("Topic reference: {:?}", topic_captures_value.clone());
                }
            }
            if let Some(book_captures) = book_reference_pattern.captures(&line) {
                if let Some(book_captures_value) =
                    book_captures.get(1).map(|m| m.as_str().trim().to_string())
                {
                    added_exercise_result.set_book_reference(book_captures_value.clone());
                    debug!("Book reference: {:?}", book_captures_value.clone());
                }
            }
            if let Some(chapter_captures) = chapter_reference_pattern.captures(&line) {
                if let Some(chapter_captures_value) = chapter_captures
                    .get(1)
                    .map(|m| m.as_str().trim().to_string())
                {
                    added_exercise_result.set_chapter_reference(chapter_captures_value.clone());
                    debug!("Chapter reference: {:?}", chapter_captures_value.clone());
                }
            }
            if let Some(section_captures) = section_reference_pattern.captures(&line) {
                if let Some(section_captures_value) = section_captures
                    .get(1)
                    .map(|m| m.as_str().trim().to_string())
                {
                    added_exercise_result.set_section_reference(section_captures_value.clone());
                    debug!("Section reference: {:?}", section_captures_value.clone());
                }
            }
        }
        debug!("Parsed added exercise: {:#?}", added_exercise_result);
        added_exercises.push(added_exercise_result);
    }
    Ok(added_exercises)
}

#[derive(Clone, Debug, Serialize, Deserialize)]
pub struct AddedExercise {
    id: i64,
    exercise_type: String,
    topic_reference: String,
    book_reference: String,
    chapter_reference: String,
    section_reference: String,
    manual_id: i32,
    question: String,
    solution: String,
}

impl AddedExercise {
    fn new() -> Self {
        Self {
            id: 0,
            exercise_type: String::from(""),
            topic_reference: String::from(""),
            book_reference: String::from(""),
            chapter_reference: String::from(""),
            section_reference: String::from(""),
            manual_id: 0,
            question: String::from(""),
            solution: String::from(""),
        }
    }

    pub fn id(&self) -> i64 {
        self.id
    }

    pub fn exercise_type(&self) -> &str {
        &self.exercise_type
    }

    pub fn topic_reference(&self) -> &str {
        &self.topic_reference
    }

    fn set_topic_reference(&mut self, topic_reference: String) {
        self.topic_reference = topic_reference;
    }

    pub fn book_reference(&self) -> &str {
        &self.book_reference
    }

    fn set_book_reference(&mut self, book_reference: String) {
        self.book_reference = book_reference;
    }

    pub fn chapter_reference(&self) -> &str {
        &self.chapter_reference
    }

    fn set_chapter_reference(&mut self, chapter_reference: String) {
        self.chapter_reference = chapter_reference;
    }

    pub fn section_reference(&self) -> &str {
        &self.section_reference
    }

    fn set_section_reference(&mut self, section_reference: String) {
        self.section_reference = section_reference;
    }

    pub fn manual_id(&self) -> i32 {
        self.manual_id
    }

    pub fn question(&self) -> &str {
        &self.question
    }

    pub fn solution(&self) -> &str {
        &self.solution
    }
}
