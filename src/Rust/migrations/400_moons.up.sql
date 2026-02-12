-- Add up migration script here
CREATE TABLE moons (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    details JSONB NOT NULL,
    planet_id BIGINT,

    PRIMARY KEY (id),
    FOREIGN KEY (planet_id) REFERENCES planets (id)
);
