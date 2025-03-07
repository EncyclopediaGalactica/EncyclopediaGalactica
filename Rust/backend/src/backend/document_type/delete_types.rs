use serde::{Deserialize, Serialize};

use super::{DocumentType, ToDocumentType};

#[derive(Deserialize, Serialize)]
pub struct DeleteDocumentTypeInput {
    pub id: i64,
}

impl ToDocumentType for DeleteDocumentTypeInput {
    fn to_document_type(self) -> DocumentType {
        DocumentType::new(self.id, String::from(""))
    }
}

impl From<DeleteDocumentTypeInput> for DocumentType {
    fn from(value: DeleteDocumentTypeInput) -> Self {
        DocumentType::new(value.id, String::from(""))
    }
}
