use std::env;

use actix_web::{middleware::Logger, App, HttpServer};
use ec::prep_test_application;

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    env::set_var("RUST_LOG", "actix_web=debug,actix_server=info");
    env_logger::init();

    HttpServer::new(move || {
        App::new()
            .wrap(Logger::default())
            .configure(prep_test_application)
    })
    .bind(("127.0.0.1", 8080))?
    .run()
    .await
}
// rustfmt: skip end
