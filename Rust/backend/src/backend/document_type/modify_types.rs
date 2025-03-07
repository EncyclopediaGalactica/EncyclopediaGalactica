use std::fmt::Display;

use serde::{Deserialize, Serialize};

use super::{DocumentType, ToDocumentType};

#[derive(Deserialize, Serialize, Debug)]
pub struct ModifyDocumentTypeInput {
    pub id: i64,
    pub name: String,
}

impl ModifyDocumentTypeInput {
    pub fn new(
        id: i64,
        name: String,
    ) -> ModifyDocumentTypeInput {
        ModifyDocumentTypeInput { id, name }
    }
}

impl ToDocumentType for ModifyDocumentTypeInput {
    fn to_document_type(self) -> DocumentType {
        DocumentType::new(self.id, self.name)
    }
}

impl From<ModifyDocumentTypeInput> for DocumentType {
    fn from(value: ModifyDocumentTypeInput) -> Self {
        DocumentType::new(value.id, value.name)
    }
}

impl Display for ModifyDocumentTypeInput {
    fn fmt(
        &self,
        f: &mut std::fmt::Formatter<'_>,
    ) -> std::fmt::Result {
        write!(
            f,
            "ModifyDocumentTypeInput:  id: {}, name: {}",
            self.id, self.name
        )
    }
}
