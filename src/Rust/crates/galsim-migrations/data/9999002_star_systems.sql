DO $$
DECLARE
  solar_system_id BIGINT;
BEGIN 
  INSERT INTO star_systems (details)
  VALUES ('{"details": {} }')
  RETURNING id
  INTO solar_system_id;

  RAISE NOTICE 'Solar System Id: %', solar_system_id;

  UPDATE star_systems
  SET 
      details = jsonb_set(
          details,
          '{ name }',
          '"Solar System"'::jsonb
      )
  WHERE id = solar_system_id;

  UPDATE star_systems
  SET 
    details = jsonb_set(
      details,
      '{ description }',
      '"The cradle of humanity"'::jsonb
    )
  WHERE id = solar_system_id;

  UPDATE star_systems
  SET
      details = jsonb_set(
          details,
          '{ x }',
          '26316.4'::jsonb
      )
  WHERE id = solar_system_id;

  UPDATE star_systems
  SET
      details = jsonb_set(
          details,
          '{ y }',
          '27946.9'::jsonb
      )
  WHERE id = solar_system_id;

  UPDATE star_systems
  SET
      details = jsonb_set(
          details,
          '{ z }',
          '45.2'::jsonb
      )
  WHERE id = solar_system_id;
END $$;
