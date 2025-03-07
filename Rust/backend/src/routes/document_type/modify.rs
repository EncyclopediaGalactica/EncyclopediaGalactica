use actix_web::{put, web, HttpResponse, Responder};
use log::debug;
use sqlx::SqlitePool;

use crate::backend::document_type::{modify::modify, modify_types::ModifyDocumentTypeInput};

#[put("/document_type")]
pub async fn modify_document_type_route(
    pool: web::Data<SqlitePool>,
    input: web::Json<ModifyDocumentTypeInput>,
) -> impl Responder {
    match modify(pool, input.into_inner()).await {
        Err(e) => {
            debug!(
                "Error happened while executing document_type.modify. Error: {:#?}",
                e
            );
            HttpResponse::BadRequest().body(format!("{:#?}", e))
        },
        Ok(result) => {
            let Ok(serialized_result) = serde_json::to_string(&result) else {
                debug!("Error happened while serializing result for document_type.modify.");
                return HttpResponse::BadRequest().body("Serialization error");
            };
            HttpResponse::Ok().body(serialized_result)
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
        backend::document_type::{
            add_types::AddDocumentTypeInput, modify_types::ModifyDocumentTypeInput,
            DocumentTypeResult,
        },
        prep_test_application, prep_test_database,
    };

    #[actix_web::test]
    async fn invalid_input() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let test_data = vec![
            ModifyDocumentTypeInput::new(0, String::from("asd")),
            ModifyDocumentTypeInput::new(1, String::from("")),
            ModifyDocumentTypeInput::new(1, String::from("as")),
            ModifyDocumentTypeInput::new(1, String::from(" as")),
            ModifyDocumentTypeInput::new(1, String::from("as ")),
            ModifyDocumentTypeInput::new(1, String::from(" as ")),
        ];

        for input in test_data {
            let reqest = test::TestRequest::put()
                .uri("/document_type")
                .set_payload(serde_json::to_string(&input).expect("Failed serialization"))
                .insert_header(ContentType::json())
                .to_request();
            let response = test::call_service(&app, reqest).await;
            assert_eq!(response.status(), StatusCode::BAD_REQUEST);
        }
    }

    #[actix_web::test]
    async fn no_such_item() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let input = ModifyDocumentTypeInput::new(0, String::from("asd"));
        let request = test::TestRequest::put()
            .uri("/document_type")
            .set_payload(serde_json::to_string(&input).expect("Failed serialization"))
            .insert_header(ContentType::json())
            .to_request();
        let response = test::call_service(&app, request).await;
        assert_eq!(response.status(), StatusCode::BAD_REQUEST);
    }
    #[actix_web::test]
    async fn modify() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let data = AddDocumentTypeInput::new(String::from("data"));
        let data_request = test::TestRequest::post()
            .uri("/document_type")
            .insert_header(ContentType::json())
            .set_payload(serde_json::to_string(&data).expect("Failed serialization"))
            .to_request();
        let data_response = test::call_service(&app, data_request).await;
        let data_response_status = data_response.status();
        let data_response_details: DocumentTypeResult = test::read_body_json(data_response).await;
        assert_eq!(data_response_status, StatusCode::OK);

        let modify_data =
            ModifyDocumentTypeInput::new(data_response_details.id, String::from("data"));
        let modify_request = test::TestRequest::put()
            .uri("/document_type")
            .set_payload(serde_json::to_string(&modify_data).expect("Failed serialization"))
            .insert_header(ContentType::json())
            .to_request();
        let modify_response = test::call_service(&app, modify_request).await;
        assert_eq!(modify_response.status(), StatusCode::OK);
        let modify_response_details: DocumentTypeResult =
            test::read_body_json(modify_response).await;
        assert_eq!(modify_response_details.id, modify_data.id);
        assert_eq!(modify_response_details.name, modify_data.name);
    }
}
