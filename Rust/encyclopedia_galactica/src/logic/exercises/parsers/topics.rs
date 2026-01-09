use std::fs::read_to_string;
use std::path::PathBuf;

use serde::Deserialize;
use serde::Serialize;
use toml::from_str;

pub fn parse_topic_files(files: Vec<PathBuf>) -> anyhow::Result<Vec<Topic>> {
    let mut topics = Vec::new();
    if files.is_empty() {
        anyhow::bail!("Expected list of topics")
    } else {
        for path in files.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<Topic>(&r) {
                Ok(parsed) => {
                    topics.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}. Error: {}", &r, e)
                }
            }
        }
    }
    Ok(topics)
}

#[derive(Clone, Debug, Serialize, Deserialize)]
pub struct Topic {
    name: String,
    reference: String,
}

impl Topic {
    pub fn name(&self) -> &str {
        &self.name
    }

    pub fn set_name(&mut self, name: String) {
        self.name = name;
    }

    pub fn reference(&self) -> &str {
        &self.reference
    }

    pub fn set_reference(&mut self, reference: String) {
        self.reference = reference;
    }
}
