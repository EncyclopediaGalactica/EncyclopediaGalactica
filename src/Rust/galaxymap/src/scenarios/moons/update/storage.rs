use anyhow::Context;
use log::debug;
use serde_json::json;
use sqlx::PgPool;
use sqlx::types::Json;

use crate::scenarios::moons::MoonEntity;
use crate::scenarios::moons::MoonEntityDetails;

pub async fn update_in_storage(
    input: MoonEntity,
    db_connection: PgPool,
) -> anyhow::Result<MoonEntity> {
    let result: MoonEntity = sqlx::query_as!(
        MoonEntity,
        r#"
            UPDATE 
                moons
            SET 
                details = $1
            WHERE 
                id = $2
            RETURNING 
                id, 
                details as "details: Json<MoonEntityDetails>"
        "#,
        json!(input.details.0),
        input.id
    )
    .fetch_one(&db_connection)
    .await
    .with_context(|| format!("Failed to update moon: (id: {:?})", input.id))?;

    debug!("Moon table: entity updated with id: {:?}", result.id);
    Ok(result)
}

#[cfg(test)]
mod tests {
    use super::*;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_update_in_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add a moon to have an existing ID
        let add_data = MoonEntityDetails::new(
            "Original Moon".to_string(),
            "Original Description".to_string(),
        );
        let add_input = MoonEntity::new(0, Json(add_data));
        let added = crate::scenarios::moons::add::storage::add_to_storage(add_input, pool.clone())
            .await
            .unwrap();

        // Now update it
        let update_data = MoonEntityDetails::new(
            "Updated Moon".to_string(),
            "Updated Description".to_string(),
        );
        let update_input = MoonEntity::new(added.id, Json(update_data));
        let updated = update_in_storage(update_input, pool.clone()).await.unwrap();

        assert_eq!(updated.id, added.id);
        assert_eq!(updated.details.name, "Updated Moon");
        assert_eq!(updated.details.description, "Updated Description");
        Ok(())
    }
}
