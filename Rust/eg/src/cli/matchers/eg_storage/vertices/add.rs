use clap::ArgMatches;
use tabled::builder::Builder;

use crate::logic::eg_storage::repository::get_connection;
use crate::logic::eg_storage::scenarios::vertices::add::AddVertexScenarioInput;
use crate::logic::eg_storage::scenarios::vertices::add::add;

pub async fn eg_storage_vertices_add(args: ArgMatches) -> anyhow::Result<()> {
    let db_connection = get_connection(&args.get_one::<String>("db-connection").unwrap()).await?;
    let data = args.get_one::<String>("data").unwrap();
    let scenario_input = AddVertexScenarioInput {
        data: data.to_string(),
    };
    add(db_connection, scenario_input).await?;
    show_results();
    Ok(())
}

fn show_results() {
    let mut table_builder = Builder::with_capacity(2, 2);
    table_builder.push_record(["Operation:", "Add vertex."]);
    table_builder.push_record(["Result:", "Success"]);
    println!("{}", table_builder.build());
}
