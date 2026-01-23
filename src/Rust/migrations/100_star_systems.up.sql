-- Add up migration script here
DROP TABLE IF EXISTS star_systems;
CREATE TABLE star_systems (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    data JSONB NOT NULL,

    PRIMARY KEY (id)
);
