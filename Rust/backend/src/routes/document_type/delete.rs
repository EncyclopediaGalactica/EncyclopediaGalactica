use actix_web::{delete, web, HttpResponse, Responder};
use sqlx::SqlitePool;

use crate::backend::document_type::{
    delete::delete_document_type, delete_types::DeleteDocumentTypeInput,
};

#[delete("/document_type")]
pub async fn delete_document_type_route(
    pool: web::Data<SqlitePool>,
    input: web::Json<DeleteDocumentTypeInput>,
) -> impl Responder {
    match delete_document_type(pool, input.into_inner()).await {
        Err(err) => HttpResponse::BadRequest().body(format!("{:#?}", err)),
        Ok(_r) => HttpResponse::Ok().body(""),
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
        backend::document_type::{
            add_types::AddDocumentTypeInput, delete_types::DeleteDocumentTypeInput,
            DocumentTypeResult,
        },
        prep_test_application, prep_test_database,
    };

    #[actix_web::test]
    async fn delete_invalid_input() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let input = vec![AddDocumentTypeInput {
            name: String::from("asd"),
        }];

        for payload in input {
            let req = test::TestRequest::delete()
                .uri("/document_type")
                .set_payload(serde_json::to_string(&payload).expect("Failed json serialization"))
                .to_request();
            let response = test::call_service(&app, req).await;
            assert_eq!(response.status(), StatusCode::BAD_REQUEST);
        }
    }

    #[actix_web::test]
    async fn delete() {
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

        let addition_request = test::TestRequest::post()
            .uri("/document_type")
            .insert_header(ContentType::json())
            .set_payload(serde_json::to_string(&payload).expect("Failed json serialization"))
            .to_request();
        let addition_response = test::call_service(&app, addition_request).await;
        assert_eq!(
            addition_response.status(),
            StatusCode::OK,
            "Error message: {:#?}",
            addition_response
        );

        let addition_response_body: DocumentTypeResult =
            test::read_body_json(addition_response).await;
        let deletion_payload = DeleteDocumentTypeInput {
            id: addition_response_body.id,
        };

        let deletion_request = test::TestRequest::delete()
            .uri("/document_type")
            .set_payload(
                serde_json::to_string(&deletion_payload).expect("Failed json serialization"),
            )
            .insert_header(ContentType::json())
            .to_request();
        let deletion_response = test::call_service(&app, deletion_request).await;
        assert_eq!(deletion_response.status(), StatusCode::OK);
    }
}
