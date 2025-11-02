CREATE TABLE IF NOT EXISTS edges (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    edge_type_id BIGINT NOT NULL,
    from_vertex_id BIGINT NOT NULL,
    to_vertex_id BIGINT NOT NULL,

    PRIMARY KEY (id),

    CONSTRAINT fk_edge_type_id
    FOREIGN KEY (edge_type_id)
    REFERENCES edge_types (id),

    CONSTRAINT fk_from_vertex_id
    FOREIGN KEY (from_vertex_id)
    REFERENCES edges (id),

    CONSTRAINT fk_to_vertex_id
    FOREIGN KEY (to_vertex_id)
    REFERENCES edges (id)
);
