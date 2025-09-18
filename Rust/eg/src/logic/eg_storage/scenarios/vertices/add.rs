use log::debug;
use serde_json::json;
use sqlx::FromRow;

use crate::logic::eg_storage::repository::get_connection;
use crate::logic::eg_storage::repository::vertices::VertexEntity;
use crate::logic::eg_storage::repository::vertices::add::add_vertex;

pub async fn add_vertex_scenario(input: AddVertexScenarioInput) -> anyhow::Result<()> {
    debug!("scenario with input: {:?}", input);
    let db_connection = get_connection(&input.database_connection_string).await?;
    debug!("Connected to database");
    let affected_rows =
        add_vertex(VertexEntity::new(json!(input.data.clone())), db_connection).await?;
    show_results(affected_rows);
    Ok(())
}

fn show_results(affected_rows: u64) {
    let mut table_builder = tabled::builder::Builder::with_capacity(2, 2);
    table_builder.push_record(["Operation:", "Add vertex."]);
    table_builder.push_record(["Result:", "Success"]);
    table_builder.push_record(["Affected rows:", &affected_rows.to_string()]);
    println!("{}", table_builder.build());
}

#[derive(Debug, Clone)]
pub struct AddVertexScenarioInput {
    pub data: String,
    pub database_connection_string: String,
}

#[derive(Debug, Clone, FromRow)]
pub struct AddVertexScenarioResult {
    pub data: serde_json::Value,
}
