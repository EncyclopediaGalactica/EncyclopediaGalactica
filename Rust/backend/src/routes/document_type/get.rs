use actix_web::{get, web, HttpResponse, Responder};
use log::{debug, info};
use sqlx::SqlitePool;

use crate::backend::document_type::{get::get_document_types, ToDocumentTypeResultVec};

#[get("/document_type")]
pub async fn get_document_types_route(pool: web::Data<SqlitePool>) -> impl Responder {
    match get_document_types(pool).await {
        Err(e) => {
            debug!("whatever");
            HttpResponse::BadRequest().body(format!("{:#?}", e))
        },
        Ok(r) => {
            debug!("debub message here whatever");
            info!("info message here whatever");
            HttpResponse::Ok().body(
                serde_json::to_string(&r.to_document_type_result_vec())
                    .expect("Error at serialization."),
            )
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
        backend::document_type::{add_types::AddDocumentTypeInput, DocumentTypeResult},
        prep_test_application, prep_test_database,
    };

    #[actix_web::test]
    async fn returns_list() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let test_data = vec![
            AddDocumentTypeInput::new(String::from("first")),
            AddDocumentTypeInput::new(String::from("second")),
            AddDocumentTypeInput::new(String::from("third")),
        ];

        for item in &test_data {
            let create_request = test::TestRequest::post()
                .uri("/document_type")
                .insert_header(ContentType::json())
                .set_payload(serde_json::to_string(&item).expect("Failed serialization"))
                .to_request();
            let create_response = test::call_service(&app, create_request).await;
            assert_eq!(
                create_response.status(),
                StatusCode::OK,
                "Failed data creation: {:#?}",
                create_response
            );
        }

        let test_request = test::TestRequest::get().uri("/document_type").to_request();
        let test_response = test::call_service(&app, test_request).await;
        let deser_result: Vec<DocumentTypeResult> = test::read_body_json(test_response).await;
        assert_ne!(deser_result.len(), 0);
        assert_eq!(deser_result.len(), 3);

        for item in test_data {
            let hit = deser_result.iter().any(|i| i.name == item.name);
            assert!(hit);
        }
    }

    #[actix_web::test]
    async fn returns_empty_list() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let test_request = test::TestRequest::get().uri("/document_type").to_request();
        let test_response = test::call_service(&app, test_request).await;
        let deser_result: Vec<DocumentTypeResult> = test::read_body_json(test_response).await;
        assert_eq!(deser_result.len(), 0);
    }
}
