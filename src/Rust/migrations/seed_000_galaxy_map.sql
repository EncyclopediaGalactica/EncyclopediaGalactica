WITH details_id AS (
    INSERT INTO galaxy_map (details) VALUES ('{"details": {}')
    RETURNING id
)

UPDATE galaxy_map
SET
    details = jsonb_set(
        details,
        '{details, baricenter}',
        '"Sagittarius A*"'::jsonb
    )
WHERE id = details_id;

UPDATE galaxy_map
SET
    details = jsonb_set(
        details,
        '{details, baricenter}',
        $$"
The center of the Milky Way galaxy is the Sagittarius A* supermassive black hole, which is also 
the galaxy's baricenter.
"$$::jsonb
    )
WHERE id = details_id;

UPDATE galaxy_map
SET
    details = jsonb_set(
        details,
        '{details, x_axis}',
        $$"
Passes through the Sagittarius A*.
Milky Way center bar''s Scutum-Centaurus end. 
The line passes through the middle of the bar. 
The middle is measured by H2 density.
"$$::jsonb
    )
WHERE id = details_id;

UPDATE galaxy_map
SET
    details = jsonb_set(
        details,
        '{details, y_axis}',
        $$"
Perpendicular to the x-axis at Sagittarius A*.
Passes through the Sagittarius A*.
"$$::jsonb
    )
WHERE id = details_id;

UPDATE galaxy_map
SET
    details = jsonb_set(
        details,
        '{details, z_axis}',
        $$"
Perpendicular to the x and y axes at Sagittarius A*.
Passes through the Sagittarius A*.
"$$::jsonb
    )
WHERE id = details_id;

UPDATE galaxy_map
SET
    details = jsonb_set(
        details,
        '{details, star_systems}',
        $$"
Star systems represented by their baricenters and not by their stars.
The baricenter is the rotational center of a star system.
"$$::jsonb
    )
WHERE id = details_id;
