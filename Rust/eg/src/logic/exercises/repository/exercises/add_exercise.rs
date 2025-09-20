use sqlx::Pool;
use sqlx::Postgres;

use crate::logic::exercises::repository::exercises::ExerciseEntity;

pub async fn add_exercise(
    exercise: ExerciseEntity,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    match sqlx::query(
        r#"
        INSERT INTO exercises (
            id_in_the_book,
            exercise_type,
            topic_id,
            book_id,
            chapter_id,
            section_id
        ) VALUES (
            $1,
            $2,
            $3,
            $4,
            $5,
            $6
        )
        "#,
    )
    .bind(exercise.id_in_book)
    .bind(exercise.exercise_type)
    .bind(exercise.topic_id)
    .bind(exercise.book_id)
    .bind(exercise.chapter_id)
    .bind(exercise.section_id)
    .execute(&db_connection)
    .await
    {
        Ok(_) => Ok(()),
        Err(nope) => Err(anyhow::anyhow!("Failed to add exercise: {:#?}", nope)),
    }
}
