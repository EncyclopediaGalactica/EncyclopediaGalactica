pub mod document_type;

#[derive(Debug)]
pub struct BackendError {
    pub message: String,
    pub trace: String,
}

impl BackendError {
    pub fn new(message: String, trace: String) -> Self {
        BackendError { message, trace }
    }
}
