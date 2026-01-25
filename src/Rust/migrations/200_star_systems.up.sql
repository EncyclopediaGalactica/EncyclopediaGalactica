-- Add up migration script here
CREATE TABLE star_systems (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    details JSONB NOT NULL,

    PRIMARY KEY (id)
);
