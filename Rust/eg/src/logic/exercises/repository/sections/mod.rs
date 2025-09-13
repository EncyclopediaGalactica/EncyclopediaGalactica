use serde::Deserialize;
use serde::Serialize;

use crate::logic::exercises::parsers::sections::Section;

pub mod add;
pub mod truncate;

#[derive(Clone, Debug, Serialize, Deserialize)]
pub struct SectionEntity {
    pub id: i64,
    pub section_title: String,
    pub section_number: f64,
    pub page_start: i32,
    pub page_exercises_start: i32,
    pub concepts_questions_interval_start: i32,
    pub concepts_questions_interval_end: i32,
    pub skills_questions_interval_start: i32,
    pub skills_questions_interval_end: i32,
    pub applications_questions_interval_start: i32,
    pub applications_questions_interval_end: i32,
    pub discussion_questions_interval_start: i32,
    pub discussion_questions_interval_end: i32,
    pub page_end: i32,
    pub chapter_id: i64,
}

impl From<Section> for SectionEntity {
    fn from(section: Section) -> Self {
        SectionEntity {
            id: 0,
            section_title: section.title().to_string(),
            section_number: section.section_number(),
            page_start: section.page_start(),
            page_exercises_start: section.page_exercises_start(),
            concepts_questions_interval_start: parse_str_to_int32(
                section.concepts_questions_interval_start(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse concepts interval start")),
            concepts_questions_interval_end: parse_str_to_int32(
                section.concepts_questions_interval_end(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse concepts interval end")),
            skills_questions_interval_start: parse_str_to_int32(
                section.skills_questions_interval_start(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse skills interval start")),
            skills_questions_interval_end: parse_str_to_int32(
                section.skills_questions_interval_end(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse skills interval end")),
            applications_questions_interval_start: parse_str_to_int32(
                section.applications_questions_interval_start(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse applications interval start")),
            applications_questions_interval_end: parse_str_to_int32(
                section.applications_questions_interval_end(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse applications interval end")),
            discussion_questions_interval_start: parse_str_to_int32(
                section.discussion_questions_interval_start(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse discussion interval start")),
            discussion_questions_interval_end: parse_str_to_int32(
                section.discussion_questions_interval_end(),
            )
            .unwrap_or_else(|_| panic!("Cannot parse discussion interval end")),
            page_end: section.page_end(),
            chapter_id: 0,
        }
    }
}

pub fn parse_str_to_int32(s: &str) -> anyhow::Result<i32> {
    if s == "NA" {
        Ok(0)
    } else {
        let parsed = s.parse::<i32>()?;
        Ok(parsed)
    }
}
