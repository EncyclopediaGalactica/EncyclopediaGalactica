use serde::Deserialize;

#[derive(Clone, Debug, Deserialize)]
pub struct AppConfig {
    pub galnav_storage_config: GalNavStroageConfig,
}

#[derive(Clone, Debug, Deserialize)]
pub struct GalNavStroageConfig {
    pub db_connection_string: String,
}
