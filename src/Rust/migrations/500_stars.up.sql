-- Add up migration script here
DROP TABLE IF EXISTS stars;
CREATE TABLE stars (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    data JSONB NOT NULL,
    star_system_id BIGINT,

    PRIMARY KEY (id),
    FOREIGN KEY (star_system_id) REFERENCES star_systems (id)
);