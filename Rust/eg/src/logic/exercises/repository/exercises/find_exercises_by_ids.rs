use log::debug;
use sqlx::FromRow;
use sqlx::Pool;
use sqlx::Postgres;
use sqlx::query_as;

pub async fn find_exercises_by_ids(
    ids: Vec<i64>,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<Vec<EnrichedExerciseEntity>> {
    let ids_string: String = ids
        .iter()
        .map(|id| id.to_string())
        .collect::<Vec<String>>()
        .join(",");
    debug!("ids_string: {:#?}", ids_string);

    match query_as::<_, EnrichedExerciseEntity>(
        r#"
        SELECT 
            e.id_in_the_book as id_in_book,
            e.exercise_type as exercise_type,
            t.name as topic_name,
            b.title as book_title,
            c.title as chapter_title,
            s.section_number as section_number,
            s.section_title as section_title,
            s.page_exercises_start as section_page_exercises_start
        FROM 
            exercises as e,
            topics as t,
            books as b,
            chapters as c,
            sections as s
        WHERE 
            e.id = ANY($1)
            AND e.topic_id = t.id
            AND e.book_id = b.id
            AND e.chapter_id = c.id
            AND e.section_id = s.id
    "#,
    )
    .bind(&ids)
    .fetch_all(&db_connection)
    .await
    {
        Ok(yolo) => Ok(yolo),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to find exercises by ids: {:#?} at {}:{}",
            nope,
            file!(),
            line!()
        )),
    }
}

#[derive(Debug, Clone, FromRow)]
pub struct EnrichedExerciseEntity {
    pub id_in_book: i32,
    pub exercise_type: String,
    pub topic_name: String,
    pub book_title: String,
    pub chapter_title: String,
    pub section_number: f64,
    pub section_title: String,
    pub section_page_exercises_start: i32,
}
