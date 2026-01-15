use anyhow::Context;
use sqlx::PgPool;

use crate::scenarios::star_systems::StarSystemEntity;

pub async fn get_all_from_storage(db_connection: PgPool) -> anyhow::Result<Vec<StarSystemEntity>> {
    let star_systems: Vec<StarSystemEntity> =
        sqlx::query_as("SELECT id, name, description, x, y, z FROM star_systems")
            .fetch_all(&db_connection)
            .await
            .with_context(|| "Failed to get all star systems")?;

    Ok(star_systems)
}

#[cfg(test)]
mod tests {
    use super::*;
    use crate::scenarios::star_systems::StarSystemEntity;
    use crate::scenarios::star_systems::add::storage::add_to_storage;
    use sqlx::PgPool;

    #[sqlx::test(migrations = "./../migrations")]
    async fn test_get_all_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        // First, add some star systems
        let add_input1 =
            StarSystemEntity::new(0, "Star System 1".to_string(), "Description 1".to_string(), Some(0.0), Some(0.0), Some(0.0));
        let _ = add_to_storage(add_input1, pool.clone()).await.unwrap();

        let add_input2 =
            StarSystemEntity::new(0, "Star System 2".to_string(), "Description 2".to_string(), Some(0.0), Some(0.0), Some(0.0));
        let _ = add_to_storage(add_input2, pool.clone()).await.unwrap();

        // Now get all
        let all = get_all_from_storage(pool.clone()).await.unwrap();

        assert!(all.len() >= 2);
        // Check that our added ones are there
        let names: Vec<String> = all.iter().map(|s| s.name.clone()).collect();
        assert!(names.contains(&"Star System 1".to_string()));
        assert!(names.contains(&"Star System 2".to_string()));
        Ok(())
    }
}
