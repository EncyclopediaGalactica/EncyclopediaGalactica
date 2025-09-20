pub async fn truncate_exercises_table(
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<()> {
    match sqlx::query(
        r#"
        TRUNCATE TABLE exercises CASCADE
        "#,
    )
    .execute(&db_connection)
    .await
    {
        Ok(_) => Ok(()),
        Err(nope) => Err(anyhow::anyhow!(
            "Failed to truncate exercises table: {:#?}",
            nope
        )),
    }
}
