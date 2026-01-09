use serde::Deserialize;

pub mod eg_storage;
pub mod exercises;
pub mod starmap;

#[derive(Deserialize, Clone, Debug)]
pub struct AppConfig {
    pub exercises: ExercisesConfig,
    pub eg_storage: EGStorageConfig,
}

#[derive(Deserialize, Clone, Debug)]
pub struct EGStorageConfig {
    pub database_connection_string: String,
}
#[derive(Deserialize, Clone, Debug)]
pub struct ExercisesConfig {
    pub database_connection_string: String,
    pub catalog_path: String,
    pub generated_tests_path: String,
}
