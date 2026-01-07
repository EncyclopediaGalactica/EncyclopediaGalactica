use clap::ArgMatches;
use log::debug;

use crate::cli::matchers::set_cli_logging_level;
use crate::logic::AppConfig;
use crate::logic::eg_storage::scenarios::vertices::add::AddVertexScenarioInput;
use crate::logic::eg_storage::scenarios::vertices::add::add_vertex_scenario;

pub async fn eg_storage_vertices_add_matcher(
    args: ArgMatches,
    config: AppConfig,
) -> anyhow::Result<()> {
    env_logger::Builder::new()
        .filter(
            None,
            set_cli_logging_level(args.clone()).unwrap_or_else(|_| log::LevelFilter::Off),
        )
        .init();
    debug!("matcher args: {:#?}", args);
    debug!("matcher config: {:#?}", config);
    let data = args.get_one::<String>("DATA").unwrap();
    let database_connection_string = config.eg_storage.database_connection_string.clone();
    let scenario_input = AddVertexScenarioInput {
        data: data.to_string(),
        database_connection_string: database_connection_string,
    };
    add_vertex_scenario(scenario_input).await?;
    Ok(())
}
