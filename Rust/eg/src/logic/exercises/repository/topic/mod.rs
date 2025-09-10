pub mod add;
pub mod truncate;

use crate::logic::exercises::parsers::topics::Topic;
use sqlx::prelude::FromRow;

#[derive(FromRow, Debug)]
pub struct TopicEntity {
    pub id: i32,
    pub name: String,
    pub reference: String,
}

impl From<Topic> for TopicEntity {
    fn from(topic: Topic) -> Self {
        TopicEntity {
            id: 0,
            name: topic.name().to_string(),
            reference: topic.reference().to_string(),
        }
    }
}
