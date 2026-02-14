use log::debug;

pub async fn truncate_chapters_table(
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<()> {
    debug!("Truncating chapters table");
    match sqlx::query("TRUNCATE TABLE chapters CASCADE")
        .execute(&db_connection)
        .await
    {
        Ok(yolo) => {
            // todo: for logging purposes
            let affected_rows = yolo.rows_affected();
            debug!(
                "truncate_chapters_table affected rows: {:#?}",
                affected_rows
            );
            Ok(())
        }
        Err(nopes) => Err(anyhow::anyhow!(
            "truncate_chapters_table nopes: {:#?} at {}:{}",
            nopes,
            file!(),
            line!()
        )),
    }
}
