use anyhow::Context;
use sqlx::PgPool;

use crate::scenarios::star_systems::StarSystemEntity;

pub async fn get_all_from_storage(db_connection: PgPool) -> anyhow::Result<Vec<StarSystemEntity>> {
    let star_systems: Vec<StarSystemEntity> = sqlx::query_as(
        r#"
        SELECT id, details 
        FROM star_systems
        "#,
    )
    .fetch_all(&db_connection)
    .await
    .with_context(|| "Failed to get all star systems")?;

    println!("star_systems: {:?}", star_systems);

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
        let data1 = serde_json::json!({
            "name": "Star System 1",
            "description": "Description 1",
            "x": 1.0,
            "y": 2.0,
            "z": 3.0
        });
        let add_input1 = StarSystemEntity::new(0, data1);
        let _ = add_to_storage(add_input1, pool.clone()).await.unwrap();

        let data2 = serde_json::json!({
            "name": "Star System 2",
            "description": "Description 2",
            "x": 2.0,
            "y": 2.0,
            "z": 2.0
        });
        let add_input2 = StarSystemEntity::new(0, data2);
        let _ = add_to_storage(add_input2, pool.clone()).await.unwrap();

        // Now get all
        let all = get_all_from_storage(pool.clone()).await.unwrap();

        assert!(all.len() >= 2);
        // Check that our added ones are there
        let names: Vec<String> = all
            .iter()
            .map(|s| {
                println!("s: {:?}", s.details);
                println!("s: {:?}", s.details["name"]);
                s.details["name"].as_str().unwrap().to_string()
            })
            .collect();
        assert!(names.contains(&"Star System 1".to_string()));
        assert!(names.contains(&"Star System 2".to_string()));
        Ok(())
    }
}
