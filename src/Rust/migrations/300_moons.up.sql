-- Add up migration script here
DROP TABLE IF EXISTS moons;
CREATE TABLE moons (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    data JSONB NOT NULL,
    planet_id BIGINT,

    PRIMARY KEY (id),
    FOREIGN KEY (planet_id) REFERENCES planets (id)
);
