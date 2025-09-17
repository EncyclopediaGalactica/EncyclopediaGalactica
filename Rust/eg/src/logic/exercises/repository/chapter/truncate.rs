pub async fn truncate_chapters_table(
    db_connection: sqlx::Pool<sqlx::Postgres>,
) -> anyhow::Result<()> {
    sqlx::query("TRUNCATE TABLE chapters CASCADE")
        .execute(&db_connection)
        .await?;
    Ok(())
}
