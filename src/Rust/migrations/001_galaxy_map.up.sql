CREATE TABLE galaxy_map (
    id BIGINT GENERATED ALWAYS AS IDENTITY,
    name TEXT,
    baricenter TEXT,
    x_axis TEXT,
    y_axis TEXT,
    z_axis TEXT,
    star_systems TEXT,

    PRIMARY KEY (id)
);
