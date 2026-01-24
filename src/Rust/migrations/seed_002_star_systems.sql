WITH solar_system_id AS (
    INSERT INTO star_systems (data)
    VALUES ('{"details": {"name": "Solar System"}}')
    RETURNING id
)

UPDATE star_systems
SET
    details = jsonb_set(
        details,
        '{details, x}'
        '26316.4'::jsonb
    )
WHERE id = solar_system_id;

UPDATE star_systems
SET
    details = jsonb_set(
        details,
        '{details, y}'
        '27946.9'::jsonb
    )
WHERE id = solar_system_id;

UPDATE star_systems
SET
    details = jsonb_set(
        details,
        '{details, y}'
        '45.2'::jsonb
    )
WHERE id = solar_system_id;
