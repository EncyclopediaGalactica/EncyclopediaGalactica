use std::fmt;
use std::str::FromStr;

use serde::Deserialize;
use serde::Serialize;
use sqlx::prelude::FromRow;

pub mod add_exercise;
pub mod find_all_raw_exercise_entities;
pub mod find_exercises_by_chapter_ids_and_type;
pub mod find_exercises_by_ids;
pub mod truncate;

#[derive(Debug, Clone, FromRow)]
pub struct ExerciseEntity {
    pub id: i64,
    pub id_in_book: i32,
    pub manual_id: i32,
    pub topic_id: i64,
    pub book_id: i64,
    pub chapter_id: i64,
    pub section_id: i64,
    pub exercise_type: String,
    pub question: String,
    pub solution: String,
}
impl ExerciseEntity {
    pub fn default() -> ExerciseEntity {
        ExerciseEntity {
            id: 0,
            id_in_book: 0,
            manual_id: 0,
            topic_id: 0,
            book_id: 0,
            chapter_id: 0,
            section_id: 0,
            exercise_type: ExerciseType::None.to_string(),
            question: String::from(""),
            solution: String::from(""),
        }
    }
}

#[derive(Debug, Clone, FromRow)]
pub struct RawExerciseEntity {
    pub topic_id: i64,
    pub book_id: i64,
    pub chapter_id: i64,
    pub section_id: i64,
    pub concepts_questions_interval_start: i32,
    pub concepts_questions_interval_end: i32,
    pub skills_questions_interval_start: i32,
    pub skills_questions_interval_end: i32,
    pub applications_questions_interval_start: i32,
    pub applications_questions_interval_end: i32,
    pub discussion_questions_interval_start: i32,
    pub discussion_questions_interval_end: i32,
}

#[derive(Debug, Clone, Copy, PartialEq, Serialize, Deserialize)]
pub enum ExerciseType {
    Concepts,
    Skills,
    Applications,
    Discussion,
    None,
}

impl FromStr for ExerciseType {
    type Err = anyhow::Error;

    fn from_str(s: &str) -> Result<Self, Self::Err> {
        match s {
            "concepts" => Ok(ExerciseType::Concepts),
            "skills" => Ok(ExerciseType::Skills),
            "applications" => Ok(ExerciseType::Applications),
            "discussion" => Ok(ExerciseType::Discussion),
            _ => Ok(ExerciseType::None),
        }
    }
}

impl fmt::Display for ExerciseType {
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        match self {
            ExerciseType::Concepts => write!(f, "concepts"),
            ExerciseType::Skills => write!(f, "skills"),
            ExerciseType::Applications => write!(f, "applications"),
            ExerciseType::Discussion => write!(f, "discussion"),
            ExerciseType::None => write!(f, "none"),
        }
    }
}
