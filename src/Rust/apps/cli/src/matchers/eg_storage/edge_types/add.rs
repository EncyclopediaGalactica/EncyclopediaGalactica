use clap::ArgMatches;
use log::debug;

use crate::cli::matchers::set_cli_logging_level;
use crate::logic::AppConfig;
use crate::logic::eg_storage::scenarios::edge_types::add::AddEdgeTypeScenarioInput;
use crate::logic::eg_storage::scenarios::edge_types::add::eg_storage_edge_types_add_scenario;

pub async fn eg_storage_edge_types_add(args: ArgMatches, config: AppConfig) -> anyhow::Result<()> {
    let scenario_input = populate_scenario_input(args.clone(), config.clone())?;
    debug!("scenario input: {:#?}", scenario_input);
    eg_storage_edge_types_add_scenario(scenario_input).await?;
    Ok(())
}

fn populate_scenario_input(
    args: ArgMatches,
    config: AppConfig,
) -> anyhow::Result<AddEdgeTypeScenarioInput> {
    let _ = set_cli_logging_level(args.clone());
    let name = args.get_one::<String>("NAME").unwrap();
    let description = args.get_one::<String>("DESC").unwrap();
    let input = AddEdgeTypeScenarioInput {
        name: name.to_string(),
        description: description.to_string(),
        db_connection_string: config.eg_storage.database_connection_string.clone(),
    };
    Ok(input)
}
