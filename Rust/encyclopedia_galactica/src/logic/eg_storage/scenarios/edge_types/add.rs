use anyhow::Ok;
use log::debug;

use crate::logic::eg_storage::repository::edge_type::EdgeTypeEntity;
use crate::logic::eg_storage::repository::edge_type::add::save_edge_type;
use crate::logic::eg_storage::repository::get_connection;

pub async fn eg_storage_edge_types_add_scenario(
    input: AddEdgeTypeScenarioInput,
) -> anyhow::Result<()> {
    debug!("input: {:#?}", input);

    let db_connection = get_connection(&input.db_connection_string).await?;
    debug!("db connection established");

    let entity = EdgeTypeEntity::from(input.clone());
    debug!("edge type entity: {:#?}", entity);

    validate_edge_type_input(&entity)?;
    debug!("edge type entity is validated");

    let result = save_edge_type(entity, db_connection).await?;
    eg_storage_edge_types_add_scenario_result(result);
    Ok(())
}

fn eg_storage_edge_types_add_scenario_result(result: u64) {
    let mut table_builder = tabled::builder::Builder::with_capacity(2, 2);
    table_builder.push_record(["Operation:", "Add edge type."]);
    table_builder.push_record(["Result:", "Success"]);
    table_builder.push_record(["Affected rows:", &result.to_string()]);
    println!("{}", table_builder.build());
}

fn validate_edge_type_input(entity: &EdgeTypeEntity) -> anyhow::Result<()> {
    if entity.name.is_empty() {
        return Err(anyhow::anyhow!("Edge type name cannot be empty."));
    }
    if entity.name.trim().len() < 3 {
        return Err(anyhow::anyhow!(
            "Edge type name must be at least 3 characters."
        ));
    }
    if entity.description.is_empty() {
        return Err(anyhow::anyhow!("Edge type description cannot be empty."));
    }
    Ok(())
}

#[derive(Debug, Clone)]
pub struct AddEdgeTypeScenarioInput {
    pub db_connection_string: String,
    pub name: String,
    pub description: String,
}

impl From<AddEdgeTypeScenarioInput> for EdgeTypeEntity {
    fn from(value: AddEdgeTypeScenarioInput) -> Self {
        EdgeTypeEntity {
            id: 0,
            name: value.name,
            description: value.description,
        }
    }
}
