pub async fn add_word_to_dictionary(input: AddWordToDictionaryInput) -> anyhow::Result<()> {
    Ok(())
}

#[derive(Debug, Clone)]
pub struct AddWordToDictionaryInput {
    pub language: String,
    pub word: String,
    pub definition: String,
}

impl AddWordToDictionaryInput {
    pub fn new(language: String, word: String, definition: String) -> Self {
        Self {
            language,
            word,
            definition,
        }
    }
}
