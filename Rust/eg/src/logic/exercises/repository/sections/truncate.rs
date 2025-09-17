pub async fn truncate_sections_table(pool: sqlx::Pool<sqlx::Postgres>) -> anyhow::Result<()> {
    sqlx::query!(
        r#"
        TRUNCATE TABLE sections CASCADE
        "#
    )
    .execute(&pool)
    .await?;
    Ok(())
}
