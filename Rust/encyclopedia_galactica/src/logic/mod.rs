use serde::Deserialize;

pub mod eg_storage;
pub mod exercises;
pub mod starmap;

#[derive(Deserialize, Clone, Debug)]
pub struct AppConfig {
    exercises: ExercisesConfig,
    eg_storage: EGStorageConfig,
}

#[derive(Deserialize, Clone, Debug)]
pub struct EGStorageConfig {
    database_connection_string: String,
}
#[derive(Deserialize, Clone, Debug)]
pub struct ExercisesConfig {
    database_connection_string: String,
    catalog_path: String,
    generated_tests_path: String,
}
