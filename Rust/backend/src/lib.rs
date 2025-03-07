use std::{env, sync::Once};

use actix_web::web;
use routes::document_type::{
    add::add_document_type_route, delete::delete_document_type_route,
    get::get_document_types_route, get_by_id::get_document_type_by_id_route,
    modify::modify_document_type_route,
};
use sqlx::{sqlite::SqlitePoolOptions, SqlitePool};
static INIT: Once = Once::new();

pub mod backend;
pub mod routes;

pub async fn prep_test_database() -> web::Data<SqlitePool> {
    let pool = SqlitePoolOptions::new()
        .max_connections(1)
        .connect("sqlite::memory:")
        .await
        .expect("Cannot create connection pool");

    sqlx::query("BEGIN")
        .execute(&pool)
        .await
        .expect("Cannot start transaction!");

    sqlx::query(
        "CREATE TABLE document_type (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL
    )",
    )
    .execute(&pool)
    .await
    .expect("Cannot prepare table!");

    sqlx::query("COMMIT")
        .execute(&pool)
        .await
        .expect("Cannot commit transaction!");
    web::Data::new(pool)
}

// We need to ensure that the logging is called once and just once.
pub fn prep_test_logging() {
    INIT.call_once(|| {
        env::set_var("RUST_LOG", "none");
        env_logger::init();
    });
}

pub fn prep_test_application(cfg: &mut web::ServiceConfig) {
    prep_test_logging();
    cfg
        // document type related routes
        .service(add_document_type_route)
        .service(delete_document_type_route)
        .service(modify_document_type_route)
        .service(get_document_type_by_id_route)
        .service(get_document_types_route);
}
