pub mod add;
pub mod get_topic_id_by_reference;
pub mod truncate;

use crate::logic::exercises::parsers::topics::Topic;
use sqlx::prelude::FromRow;

#[derive(FromRow, Debug)]
pub struct TopicEntity {
    pub id: i64,
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
