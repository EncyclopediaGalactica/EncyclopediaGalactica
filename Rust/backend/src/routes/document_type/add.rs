use actix_web::{post, web, HttpResponse, Responder};
use log::error;
use sqlx::SqlitePool;

use crate::backend::document_type::{add::add, add_types::AddDocumentTypeInput};

#[post("/document_type")]
pub async fn add_document_type_route(
    pool: web::Data<SqlitePool>,
    input: web::Json<AddDocumentTypeInput>,
) -> impl Responder {
    match add(pool, input.into_inner()).await {
        Err(e) => {
            error!("Failed operation: {:#?}", e);
            HttpResponse::BadRequest().body(format!("error at handler: {:#?}", e))
        },
        Ok(r) => {
            let Ok(result) = serde_json::to_string(&r.to_document_type_result()) else {
                error!("Error during serialization");
                return HttpResponse::BadRequest().body("Error during serialization");
            };
            HttpResponse::Ok().body(result)
        },
    }
}

#[cfg(test)]
mod test {

    use actix_web::{
        http::{header::ContentType, StatusCode},
        middleware::Logger,
        test, App,
    };

    use crate::{
        backend::document_type::add_types::AddDocumentTypeInput, prep_test_application,
        prep_test_database,
    };

    #[actix_web::test]
    async fn validation_negative_tests() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let input = vec![
            AddDocumentTypeInput {
                name: String::from(""),
            },
            AddDocumentTypeInput {
                name: String::from("aa"),
            },
            AddDocumentTypeInput {
                name: String::from(" aa"),
            },
            AddDocumentTypeInput {
                name: String::from(" aa "),
            },
            AddDocumentTypeInput {
                name: String::from(" aa  "),
            },
        ];

        for payload in input {
            let Ok(serialized_payload) = serde_json::to_string(&payload) else {
                panic!("Unsuccessful serialization!");
            };
            let req = test::TestRequest::post()
                .uri("/document_type")
                .insert_header(ContentType::json())
                .set_payload(serialized_payload)
                .to_request();
            let resp = test::call_service(&app, req).await;
            assert_eq!(
                resp.status(),
                StatusCode::BAD_REQUEST,
                "Test data is {} and result is {}",
                payload,
                resp.status()
            );
        }
    }

    #[actix_web::test]
    async fn create_record() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let payload = AddDocumentTypeInput {
            name: String::from("first"),
        };

        let Ok(serialized_payload) = serde_json::to_string(&payload) else {
            panic!("Unsuccessful serialization!");
        };
        let req = test::TestRequest::post()
            .uri("/document_type")
            .insert_header(ContentType::json())
            .set_payload(serialized_payload)
            .to_request();
        let resp = test::call_service(&app, req).await;
        let resp_status_code = resp.status();
        let resp_body = String::from_utf8(test::read_body(resp).await.to_vec())
            .expect("Can't read response body");
        assert_eq!(resp_status_code, StatusCode::OK, "Error: {}", resp_body);
    }
}
