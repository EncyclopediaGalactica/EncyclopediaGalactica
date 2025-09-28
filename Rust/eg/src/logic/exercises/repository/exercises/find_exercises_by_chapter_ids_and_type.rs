use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use super::ExerciseType;

pub async fn find_exercises_by_chapter_ids_and_type(
    chapter_ids: Vec<i64>,
    exercise_type: ExerciseType,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<Vec<i64>> {
    debug!(
        "find_exercises_by_chapter_ids_and_type: chapter_ids: {:#?}, exercise_type: {:#?}",
        chapter_ids, exercise_type
    );
    match sqlx::query_as::<_, FindExercisesByChapterIdsAndType>(
        r#"
        SELECT id
        FROM exercises
        WHERE chapter_id = ANY($1)
        AND exercise_type = $2
        "#,
    )
    .bind(chapter_ids)
    .bind(exercise_type.to_string())
    .fetch_all(&db_connection)
    .await
    {
        Ok(yolo) => Ok(yolo.iter().map(|e| e.id).collect()),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to find exercises by chapter ids and type: {:#?} at {}:{}",
            nope,
            file!(),
            line!()
        )),
    }
}

#[derive(sqlx::FromRow, Debug)]
struct FindExercisesByChapterIdsAndType {
    pub id: i64,
}
