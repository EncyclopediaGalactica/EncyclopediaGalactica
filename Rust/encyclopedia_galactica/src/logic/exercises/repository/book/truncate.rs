use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

pub async fn truncate_books_table(db_connnection: Pool<Postgres>) -> anyhow::Result<()> {
    match sqlx::query("TRUNCATE TABLE books CASCADE")
        .execute(&db_connnection)
        .await
    {
        Ok(yolo) => {
            // todo: for logging purposes
            let affected_rows = yolo.rows_affected();
            debug!("truncate_books_table affected rows: {:#?}", affected_rows);
            Ok(())
        }
        Err(nopes) => {
            debug!("truncate_books_table nopes: {:#?}", nopes);
            Err(anyhow::anyhow!("truncate_books_table nopes: {:#?}", nopes))
        }
    }
}
