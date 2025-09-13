use super::RawExerciseEntity;

pub async fn find_all_raw_exercise_entities(
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<Vec<RawExerciseEntity>> {
    match sqlx::query_as::<_, RawExerciseEntity>(
        r#"
        SELECT 
            t.id as topic_id,
            b.id as book_id,
            c.id as chapter_id,
            s.id as section_id,
            s.concepts_questions_interval_start as concepts_questions_interval_start,
            s.concepts_questions_interval_end as concepts_questions_interval_end,
            s.skills_questions_interval_start as skills_questions_interval_start,
            s.skills_questions_interval_end as skills_questions_interval_end,
            s.applications_questions_interval_start as applications_questions_interval_start,
            s.applications_questions_interval_end as applications_questions_interval_end,
            s.discussion_questions_interval_start as discussion_questions_interval_start,
            s.discussion_questions_interval_end as discussion_questions_interval_end
        FROM 
            topics as t,
            books as b,
            chapters as c,
            sections as s
        WHERE
            t.id = b.topic_id
            AND b.id = c.book_id
            AND c.id = s.chapter_id 
        "#,
    )
    .fetch_all(&db_connection)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to find all raw exercise entities: {:#?}",
            nope
        )),
    }
}
