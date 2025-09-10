use std::fs;
use std::path::Path;
use std::path::PathBuf;

use anyhow::Ok;
use sqlx::Pool;
use sqlx::Postgres;

use crate::ExercisesConfig;
use crate::logic::exercises::catalog_scanner::scan_and_collect_catalog_files_by_pattern;
use crate::logic::exercises::parsers::topics::parse_topic_files;
use crate::logic::exercises::repository::get_connection;
use crate::logic::exercises::repository::topic::add::add_topic;
use crate::logic::exercises::repository::topic::truncate::truncate_topics_table;

pub async fn sync_catalog_to_db(config: ExercisesConfig) -> anyhow::Result<()> {
    let book_catalog_absolute_path = canonicalize_path_from_config(config.clone().catalog_path)?;
    let db_connection = get_connection(&config.database_connection_string).await?;
    sync_topics_to_db(book_catalog_absolute_path, db_connection).await?;
    Ok(())
}

async fn sync_topics_to_db(
    book_catalog_absolute_path: PathBuf,
    db_connection: Pool<Postgres>,
) -> anyhow::Result<()> {
    let topic_files_with_absolute_path = scan_and_collect_catalog_files_by_pattern(
        book_catalog_absolute_path.clone(),
        "topic.toml",
    )?;
    topic_files_with_absolute_path.iter().for_each(|path| {
        println!("topic path: {:#?}", path);
    });
    let parsed_topics = parse_topic_files(topic_files_with_absolute_path)?;
    if parsed_topics.is_empty() {
        anyhow::bail!("Parsed topics list is empty");
    }
    truncate_topics_table(db_connection.clone()).await?;

    for topic in parsed_topics {
        add_topic(topic.into(), db_connection.clone()).await?;
    }
    Ok(())
}

fn canonicalize_path_from_config(path: String) -> anyhow::Result<PathBuf> {
    let path = Path::new(&path);
    Ok(fs::canonicalize(path)?)
}
