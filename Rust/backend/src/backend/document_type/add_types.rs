use core::fmt;

use serde::{Deserialize, Serialize};

use super::{DocumentType, ToDocumentType};

#[derive(Deserialize, Serialize, Debug)]
pub struct AddDocumentTypeInput {
    pub name: String,
}

impl AddDocumentTypeInput {
    pub fn new(name: String) -> AddDocumentTypeInput {
        AddDocumentTypeInput { name }
    }
}

impl ToDocumentType for AddDocumentTypeInput {
    fn to_document_type(self) -> DocumentType {
        DocumentType::new(0, self.name)
    }
}

impl From<AddDocumentTypeInput> for DocumentType {
    fn from(value: AddDocumentTypeInput) -> Self {
        DocumentType::new(0, value.name)
    }
}

impl fmt::Display for AddDocumentTypeInput {
    fn fmt(
        &self,
        f: &mut std::fmt::Formatter<'_>,
    ) -> std::fmt::Result {
        write!(f, "AddDocumentTypeInput:  name: {}", self.name)
    }
}
