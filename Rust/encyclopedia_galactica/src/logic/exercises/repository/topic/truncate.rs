use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

pub async fn truncate_topics_table(db_connnection: Pool<Postgres>) -> anyhow::Result<()> {
    debug!("Truncating topics table");
    match sqlx::query("TRUNCATE TABLE topics CASCADE")
        .execute(&db_connnection)
        .await
    {
        Ok(yolo) => {
            // todo: for logging purposes
            let affected_rows = yolo.rows_affected();
            debug!("truncate_topics_table affected rows: {:#?}", affected_rows);
            Ok(())
        }
        Err(nopes) => Err(anyhow::anyhow!(
            "truncate_topics_table nopes: {:#?} at {}:{}",
            nopes,
            file!(),
            line!()
        )),
    }
}
