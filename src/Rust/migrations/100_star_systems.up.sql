-- Add up migration script here
CREATE TABLE IF NOT EXISTS star_systems (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    y FLOAT8 DEFAULT 0.0,
    x FLOAT8 DEFAULT 0.0,
    z FLOAT8 DEFAULT 0.0,

    PRIMARY KEY (id)
);
