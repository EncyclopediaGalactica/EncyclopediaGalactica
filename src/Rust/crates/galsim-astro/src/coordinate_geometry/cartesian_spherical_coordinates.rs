use anyhow::Result;

/// This function calculates a 3D coordinate in the galaxy's coordinate system.
/// The input is custom spherical coordinates and the output is the standard cartesian coordinates.
/// This is basically a little bit custom spherical coordinates transform to cartesian coodinates.
///
/// The scenario where this function can be used is when the galaxy is extended by new star
/// systems. In that case the motivation is rather adding a new star system relative to another one
/// and not juggling with the galaxy's coordinate system. The latter is not intuitive at all in
/// this scenario. So, defining a new star system like: "There is a star system 13,87 light years
/// distance from the Alfa Centauri system in 82 degree polar angle and 22 degree azimuth angle
/// direction". Based on this information the cartesian coordinates can be calculated.
///
/// The above mentioned thinking is valid when there are multiple star systems as reference
/// (possible close to each other or neighbors). For this case there will be additional functions
/// provided.
///
/// # Arguments
/// - `origin_x` - the x coordinate of the reference star system
/// - `origin_y` - the y coordinate of the reference star system
/// - `origin_z` - the z coordinate of the reference star system
/// - `polar_angle` - the polar angle component of the straight line between the reference star
/// system and the new star system. The polar angle shows the elevation. When it goes up it takes
/// positive value. When the straight line goes below the horizon it takes negative value. The
/// value can be more than 360 degrees, but from usability point of view it doesn't make any sense.
/// - `azimuth_angle` - the azimuth angle component of the straight line between the reference star
/// system and the new star system. For reference, when the azimuth angle is 0 degrees it
/// coincides with the X axis of the galaxy's coordinate system. So, when azimuth angle it means
/// the galactic center is behind us. When the azimuth angle is positive the new star system is at
/// the right side, and 180 degrees means right behind us. When the azimuth angle is negative the
/// new star system falls to the left side and 180 degree means it is right behind us.
/// Consequently, positive and negative 180 degrees in azimuth means the same thing.
/// - `distance` - the straight line between the reference star system and the new star system has
/// the given length.
///
/// # Returns
/// - `Result<f64, anyhow::Error>` - the cartesian coordinates of the new star system
///
pub async fn cartesian_spherical_coordinates_from_given_origin(
    origin_x: &f64,
    origin_y: &f64,
    origin_z: &f64,
    polar_angle: &f64,
    azimuth_angle: &f64,
    distance: &f64,
) -> Result<(f64, f64, f64)> {
    let polar_angle_sin_cos = polar_angle.to_radians().sin_cos();
    let polar_angle_sin = polar_angle_sin_cos.0;
    let polar_angle_cos = polar_angle_sin_cos.1;

    let azimuth_angle_sin_cos = azimuth_angle.to_radians().sin_cos();
    let azimuth_angle_sin = azimuth_angle_sin_cos.0;
    let azimuth_angle_cos = azimuth_angle_sin_cos.1;

    let z_coordinate = polar_angle_sin * distance;
    let x_coordinate = azimuth_angle_cos * &polar_angle_cos * distance;
    let y_coordinate = azimuth_angle_sin * &polar_angle_cos * distance;
    Ok((
        x_coordinate + origin_x,
        y_coordinate + origin_y,
        z_coordinate + origin_z,
    ))
}

#[cfg(test)]
mod tests {
    use crate::coordinate_geometry::cartesian_spherical_coordinates::cartesian_spherical_coordinates_from_given_origin;

    #[tokio::test]
    async fn test_1() {
        let cases = vec![
            (100.0, 100.0, 100.0, 60.0, 30.0, 12.0, "simple case"),
            (100.0, 100.0, 100.0, 90.0, 30.0, 12.0, "polar angle is 90"),
            (100.0, 100.0, 100.0, 91.0, 30.0, 12.0, "polar angle is 91"),
            (100.0, 100.0, 100.0, 180.0, 30.0, 12.0, "polar angle is 180"),
            (100.0, 100.0, 100.0, 181.0, 30.0, 12.0, "polar angle is 181"),
            (100.0, 100.0, 100.0, 270.0, 30.0, 12.0, "polar angle is 270"),
            (100.0, 100.0, 100.0, 271.0, 30.0, 12.0, "polar angle is 271"),
            (100.0, 100.0, 100.0, 360.0, 30.0, 12.0, "polar angle is 360"),
            (100.0, 100.0, 100.0, 361.0, 30.0, 12.0, "polar angle is 361"),
            (100.0, 100.0, 100.0, 60.0, 90.0, 12.0, "azimut angle is 90"),
            (100.0, 100.0, 100.0, 60.0, 91.0, 12.0, "azimut angle is 91"),
            (
                100.0,
                100.0,
                100.0,
                60.0,
                180.0,
                12.0,
                "azimut angle is 180",
            ),
            (
                100.0,
                100.0,
                100.0,
                60.0,
                181.0,
                12.0,
                "azimut angle is 181",
            ),
            (
                100.0,
                100.0,
                100.0,
                60.0,
                270.0,
                12.0,
                "azimut angle is 270",
            ),
            (
                100.0,
                100.0,
                100.0,
                60.0,
                271.0,
                12.0,
                "azimut angle is 271",
            ),
            (
                100.0,
                100.0,
                100.0,
                60.0,
                360.0,
                12.0,
                "azimut angle is 360",
            ),
            (
                100.0,
                100.0,
                100.0,
                60.0,
                361.0,
                12.0,
                "azimut angle is 361",
            ),
        ];
        for case in cases {
            let origin_x = case.0;
            let origin_y = case.1;
            let origin_z = case.2;
            let polar_angle = case.3;
            let azimuth_angle = case.4;
            let distance: f64 = case.5;

            let result = cartesian_spherical_coordinates_from_given_origin(
                &origin_x,
                &origin_y,
                &origin_z,
                &polar_angle,
                &azimuth_angle,
                &distance,
            )
            .await
            .unwrap();
            let calculated_distance_from_result = f64::sqrt(
                (origin_x - result.0).powi(2)
                    + (origin_y - result.1).powi(2)
                    + (origin_z - result.2).powi(2),
            );
            // the result is rounded to 6 decimals, however the values are stored in the database
            // without any rounding
            let rounded_expected_distance: f64 =
                (calculated_distance_from_result * 1_000_000.00).round() / 1_000_000.00;
            assert_eq!(
                distance, rounded_expected_distance,
                "The case: {}, expected: {}, got: {}",
                case.6, distance, calculated_distance_from_result
            );
        }
    }
}
