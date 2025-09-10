use sqlx::Pool;
use sqlx::Postgres;

pub async fn truncate_topics_table(db_connnection: Pool<Postgres>) -> anyhow::Result<()> {
    match sqlx::query("TRUNCATE TABLE topics CASCADE")
        .execute(&db_connnection)
        .await
    {
        Ok(yolo) => {
            // todo: for logging purposes
            let affected_rows = yolo.rows_affected();
            println!("affected rows: {:#?}", affected_rows);
            Ok(())
        }
        Err(nopes) => {
            println!("nopes: {:#?}", nopes);
            Err(anyhow::anyhow!("nopes: {:#?}", nopes))
        }
    }
}
