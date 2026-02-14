DO $outer$
DECLARE
  galaxy_map_id BIGINT;
BEGIN
    INSERT INTO galaxy_map (name) VALUES ('galaxy map')
    RETURNING id
    INTO galaxy_map_id;

  UPDATE galaxy_map
  SET
    baricenter = 
  $doc$
  The center of the Milky Way galaxy is the Sagittarius A* supermassive black hole,
  which is also the galaxy's baricenter.
  $doc$
  WHERE id = galaxy_map_id;

  UPDATE galaxy_map
  SET x_axis = 
  $doc$
  Passes through the Sagittarius A*.
  Milky Way center bar's Scutum-Centaurus end.
  The line passes through the middle of the bar.
  The middle is measured by H2 density.
  $doc$
  WHERE id = galaxy_map_id;

  UPDATE galaxy_map
  SET y_axis =
  $doc$
  Perpendicular to the x-axis at Sagittarius A*.
  Passes through the Sagittarius A*.
  $doc$
  WHERE id = galaxy_map_id;

  UPDATE galaxy_map
  SET z_axis =
  $doc$
  Perpendicular to the x and y axes at Sagittarius A*.
  Passes through the Sagittarius A*.
  $doc$
  WHERE id = galaxy_map_id;

  UPDATE galaxy_map
  SET star_systems =
  $doc$
  Star systems represented by their baricenters and not by their stars.
  The baricenter is the rotational center of a star system.
  $doc$
  WHERE id = galaxy_map_id;
END $outer$;
