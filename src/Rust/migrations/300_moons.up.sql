-- Add up migration script here
CREATE TABLE IF NOT EXISTS moons (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    planet_id BIGINT,

    PRIMARY KEY (id),
    FOREIGN KEY (planet_id) REFERENCES planets (id)
);
