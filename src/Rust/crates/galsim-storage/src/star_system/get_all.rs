use anyhow::Context;
use galsim_objects::star_system::entities::star_system::StarSystemEntity;
use sqlx::PgPool;

pub async fn get_all_star_systems(db_connection: PgPool) -> anyhow::Result<Vec<StarSystemEntity>> {
    let star_systems: Vec<StarSystemEntity> = sqlx::query_as::<_, StarSystemEntity>(
        r#"
        SELECT
            id,
            details
        FROM 
            star_systems
        "#,
    )
    .fetch_all(&db_connection)
    .await
    .with_context(|| "Failed to get all star systems")?;

    Ok(star_systems)
}

#[cfg(test)]
mod tests {
    use crate::star_system::add::add_star_system;

    use super::*;
    use galsim_objects::star_system::entities::star_system_details::StarSystemEntityDetails;
    use sqlx::PgPool;
    use sqlx::types::Json;

    #[sqlx::test]
    async fn test_get_all_from_storage_success(pool: PgPool) -> sqlx::Result<()> {
        sqlx::migrate!("./migrations")
            .run(&pool)
            .await
            .unwrap();
        // First, add some star systems
        let data1 = StarSystemEntityDetails::new(
            "Star System 1".to_string(),
            "Description 1".to_string(),
            Some(1.0),
            Some(2.0),
            Some(3.0),
        );
        let add_input1 = StarSystemEntity::new(0, Json(data1));
        let _ = add_star_system(add_input1, pool.clone()).await.unwrap();

        let data2 = StarSystemEntityDetails::new(
            "Star System 2".to_string(),
            "Description 2".to_string(),
            Some(2.0),
            Some(2.0),
            Some(2.0),
        );
        let add_input2 = StarSystemEntity::new(0, Json(data2));
        let _ = add_star_system(add_input2, pool.clone()).await.unwrap();

        // Now get all
        let all = get_all_star_systems(pool.clone()).await.unwrap();

        assert!(all.len() >= 2);
        // Check that our added ones are there
        let names: Vec<String> = all.iter().map(|s| s.details().name().to_string()).collect();
        assert!(names.contains(&"Star System 1".to_string()));
        assert!(names.contains(&"Star System 2".to_string()));
        Ok(())
    }
}
