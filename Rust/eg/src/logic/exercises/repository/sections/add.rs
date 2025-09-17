use crate::logic::exercises::repository::chapter::find_chapter_id_by_chapter_and_book_reference::find_chapter_id_by_chapter_and_book_reference;

use super::SectionEntity;

pub async fn add_section(
    section: SectionEntity,
    chapter_reference: &str,
    book_reference: &str,
    pool: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<()> {
    let chapter_id = find_chapter_id_by_chapter_and_book_reference(
        chapter_reference,
        book_reference,
        pool.clone(),
    )
    .await?;
    match sqlx::query(
        r#"
        INSERT INTO sections (
            section_title,
            section_number,
            page_start,
            page_exercises_start,
            concepts_questions_interval_start,
            concepts_questions_interval_end,
            skills_questions_interval_start,
            skills_questions_interval_end,
            applications_questions_interval_start,
            applications_questions_interval_end,
            discussion_questions_interval_start,
            discussion_questions_interval_end,
            page_end,
            chapter_id
        )
        VALUES (
            $1,
            $2,
            $3,
            $4,
            $5,
            $6,
            $7,
            $8,
            $9,
            $10,
            $11,
            $12,
            $13,
            $14
        )
        "#,
    )
    .bind(&section.section_title)
    .bind(&section.section_number)
    .bind(&section.page_start)
    .bind(&section.page_exercises_start)
    .bind(&section.concepts_questions_interval_start)
    .bind(&section.concepts_questions_interval_end)
    .bind(&section.skills_questions_interval_start)
    .bind(&section.skills_questions_interval_end)
    .bind(&section.applications_questions_interval_start)
    .bind(&section.applications_questions_interval_end)
    .bind(&section.discussion_questions_interval_start)
    .bind(&section.discussion_questions_interval_end)
    .bind(&section.page_end)
    .bind(&chapter_id)
    .execute(&pool)
    .await
    {
        Ok(yolo) => {
            println!("Added section affected rows: {:#?}", yolo.rows_affected());
            Ok(())
        }
        Err(nopes) => Err(anyhow::anyhow!("Failed to add section: {:#?}", nopes)),
    }
}
