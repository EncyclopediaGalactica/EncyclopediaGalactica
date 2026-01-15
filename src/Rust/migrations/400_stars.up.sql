-- Add up migration script here
CREATE TABLE IF NOT EXISTS stars (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    star_system_id BIGINT,

    PRIMARY KEY (id),
    FOREIGN KEY (star_system_id) REFERENCES star_systems (id)
);