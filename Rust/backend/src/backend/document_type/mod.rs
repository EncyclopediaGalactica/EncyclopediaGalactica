use std::fmt;

use serde::{Deserialize, Serialize};

pub mod add;
pub mod add_types;
pub mod delete;
pub mod delete_types;
pub mod get;
pub mod get_by_id;
pub mod modify;
pub mod modify_types;

pub struct DocumentType {
    pub id: i64,
    pub name: String,
}

pub trait ToDocumentTypeResultVec {
    fn to_document_type_result_vec(self) -> Vec<DocumentTypeResult>;
}

impl ToDocumentTypeResultVec for Vec<DocumentType> {
    fn to_document_type_result_vec(self) -> Vec<DocumentTypeResult> {
        self.iter().map(|e| e.into()).collect()
    }
}

impl DocumentType {
    pub fn new(
        id: i64,
        name: String,
    ) -> Self {
        DocumentType { id, name }
    }
    pub fn to_document_type_result(self) -> DocumentTypeResult {
        DocumentTypeResult::new(self.id, self.name)
    }
}

impl fmt::Display for DocumentType {
    fn fmt(
        &self,
        f: &mut fmt::Formatter<'_>,
    ) -> fmt::Result {
        write!(f, "DocumentType: id: {},  name: {}", self.id, self.name)
    }
}

trait ToDocumentType {
    fn to_document_type(self) -> DocumentType;
}

#[derive(Deserialize, Serialize, Debug)]
pub struct DocumentTypeResult {
    pub id: i64,
    pub name: String,
}

impl DocumentTypeResult {
    pub fn new(
        id: i64,
        name: String,
    ) -> Self {
        DocumentTypeResult { id, name }
    }
}

impl From<&DocumentType> for DocumentTypeResult {
    fn from(value: &DocumentType) -> Self {
        DocumentTypeResult::new(value.id, value.name.clone())
    }
}
