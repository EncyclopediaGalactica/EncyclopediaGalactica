use actix_web::{get, web, HttpResponse, Responder};
use log::debug;
use sqlx::SqlitePool;

use crate::backend::document_type::get_by_id::get_by_id;

#[get("/document_type/{id}")]
pub async fn get_document_type_by_id_route(
    pool: web::Data<SqlitePool>,
    id: web::Path<i64>,
) -> impl Responder {
    match get_by_id(pool, id.into_inner()).await {
        Ok(r) => {
            let Ok(result) = serde_json::to_string(&r.to_document_type_result()) else {
                debug!("Failed serialization");
                return HttpResponse::BadRequest().body("Failed serialization");
            };
            HttpResponse::Ok().body(result)
        },
        Err(e) => HttpResponse::BadRequest().body(format!("{:#?}", e)),
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
    async fn returns_400_when_there_is_no_hit() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let reqest = test::TestRequest::get()
            .uri("/document_type/100")
            .insert_header(ContentType::json())
            .to_request();
        let response = test::call_service(&app, reqest).await;
        let response_code = response.status();
        assert_eq!(response_code, StatusCode::BAD_REQUEST);
    }

    #[actix_web::test]
    async fn returns_200_with_result() {
        let app = test::init_service(
            App::new()
                .wrap(Logger::new("%a %{User-Agent}i"))
                .app_data(prep_test_database().await)
                .configure(prep_test_application),
        )
        .await;

        let create_reqest = test::TestRequest::post()
            .uri("/document_type")
            .set_payload(
                serde_json::to_string(&AddDocumentTypeInput::new(String::from("test")))
                    .expect("Failed serialization"),
            )
            .insert_header(ContentType::json())
            .to_request();
        let create_response = test::call_service(&app, create_reqest).await;
        let create_response_status = create_response.status();
        let create_response_content: DocumentTypeResult =
            test::read_body_json(create_response).await;
        assert_eq!(create_response_status, StatusCode::OK);

        let get_uri = format!("/document_type/{}", &create_response_content.id).to_string();

        let get_request = test::TestRequest::get()
            .uri(&get_uri)
            .insert_header(ContentType::json())
            .to_request();
        let get_response = test::call_service(&app, get_request).await;
        let get_response_status_code = get_response.status();
        let get_response_content: DocumentTypeResult = test::read_body_json(get_response).await;
        assert_eq!(get_response_status_code, StatusCode::OK);
        assert_eq!(get_response_content.id, create_response_content.id);
        assert_eq!(get_response_content.name, create_response_content.name);
    }
}
