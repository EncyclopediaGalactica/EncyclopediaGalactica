CREATE TABLE IF NOT EXISTS exercises (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    id_in_the_book INT NOT NULL,
    exercise_type VARCHAR(255) NOT NULL,
    topic_id BIGINT NOT NULL DEFAULT 0,
    book_id BIGINT NOT NULL DEFAULT 0,
    chapter_id BIGINT NOT NULL DEFAULT 0,
    section_id BIGINT NOT NULL DEFAULT 0,
    PRIMARY KEY (id),

    CONSTRAINT fk_topic_id
    FOREIGN KEY (topic_id)
    REFERENCES topics (id),

    CONSTRAINT fk_book_id
    FOREIGN KEY (book_id)
    REFERENCES books (id),

    CONSTRAINT fk_chapter_id
    FOREIGN KEY (chapter_id)
    REFERENCES chapters (id),

    CONSTRAINT fk_section_id
    FOREIGN KEY (section_id)
    REFERENCES sections (id)
);
