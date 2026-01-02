use anyhow::ensure;
use log::debug;
use sqlx::Pool;
use sqlx::Postgres;

use crate::logic::starmap::StarMapValidationResult;
use crate::logic::starmap::get_connection;

use super::PlanetEntity;

pub async fn add_planet_scenario(
    input: AddPlanetScenarioInput,
    db_connection_string: &str,
) -> anyhow::Result<AddPlanetScenarioResult> {
    validate_add_planet_scenario_input(input.clone()).await?;
    let db_connection = get_connection(db_connection_string).await?;
    let recorded_planet = add_to_storage(PlanetEntity::from(input), db_connection).await?;
    Ok(AddPlanetScenarioResult::from(recorded_planet))
}

pub async fn add_to_storage(
    input: PlanetEntity,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<PlanetEntity> {
    let result: PlanetEntity = sqlx::query_as(
        r#"
        INSERT INTO 
            planets (name, description) 
            VALUES ($1, $2)
        RETURNING id, name, description
        "#,
    )
    .bind(input.name)
    .bind(input.description)
    .fetch_one(&db_connection)
    .await?;

    debug!("Planet table: entity inserted with id: {:?}", result.id);
    Ok(result)
}

async fn validate_add_planet_scenario_input(
    input: AddPlanetScenarioInput,
) -> anyhow::Result<StarMapValidationResult> {
    ensure!(
        input.name.trim().len() < 3,
        StarMapValidationResult::fail(
            "Planet's trimmed name must be longer than 2 characters.".to_string()
        )
    );
    ensure!(
        input.description.trim().len() < 3,
        StarMapValidationResult::fail(
            "Planet's trimmed description must be longer than 2 characters.".to_string()
        )
    );

    return Ok(StarMapValidationResult::valid());
}

#[derive(Debug, Clone)]
pub struct AddPlanetScenarioInput {
    pub name: String,
    pub description: String,
}
#[derive(Debug, Clone)]
pub struct AddPlanetScenarioResult {
    pub id: i64,
    pub name: String,
    pub description: String,
}

impl AddPlanetScenarioResult {
    pub fn new(id: i64, name: String, description: String) -> Self {
        Self {
            id,
            name,
            description,
        }
    }
}

impl From<PlanetEntity> for AddPlanetScenarioResult {
    fn from(value: PlanetEntity) -> Self {
        AddPlanetScenarioResult::new(value.id, value.name, value.description)
    }
}
