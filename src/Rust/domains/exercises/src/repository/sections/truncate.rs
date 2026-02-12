use log::debug;

pub async fn truncate_sections_table(pool: sqlx::Pool<sqlx::Postgres>) -> anyhow::Result<()> {
    debug!("Truncating sections table");
    match sqlx::query(
        r#"
        TRUNCATE TABLE sections CASCADE
        "#,
    )
    .execute(&pool)
    .await
    {
        Ok(_) => Ok(()),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to truncate sections table: {:#?} at {:#?}:{:#?}",
            nope,
            file!(),
            line!(),
        )),
    }
}
