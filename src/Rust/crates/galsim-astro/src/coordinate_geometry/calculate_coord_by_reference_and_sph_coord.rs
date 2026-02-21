use anyhow::Result;

/// The function calcucates coordintes relative to a reference coordinate using the provided
/// elevation and azimuth angles and the distance. The calculation is similar to a simple from
/// spherical to cartesian coordinate transformation, but has its own customisations.
// The unique in this method is its viewpoint. It is similar to a from spherical to cartesian
// coordinate transformation, but has its own customisations.
//
// The viewpoint is set up as follows:
// - the galactic center is behind us.
// - from the galactic center, we look at the reference star system
// - the coordinates we have to provide about the new star system are relative to the reference
//
// The galactic coordinate system is where the galactic center is the origin. The position of the
// x-y plane is defined by other properties of the galaxy. In case of the Milky Way, the inner
// section has a wand and the x-y plane position is this.
// However, when we define the new star system coordinates, we do a little bit differently. The
// viewpoint (the galactic center is behind us) assumes that the x axis to the reference star
// system is a straight line. But, in realisty the x - line of the reference star system is
// parallel, so our reference view have an angle to this x-axis. At this point we ignore this. This
// would make the calculations more complicated and would not make a difference. If needed, by
// additional calculations the necessary corrections can be made.
//
// The following picture shows how this extending works:
// ![adding a new star system to the galaxy](media/adding_new_star_system_using_a_single_reference.drawio.png)
//
// The coordinates of the new star system relative to the reference star system are:
// - distance in light years - feel free to use up to 10 decimal places
// - elevation - whether the new star system is above or below the horizon. Where the horizon is
// defined by the reference star system x-y plane. Negative angle means below the horizon, while
// positive angle means above the horizon. The range is -90 to 90, But the range can be -180 to
// 180, but not recommended.
// - azimuth - where the new star system is left or right from our point of view to the reference
// star system. The negative angle means right, while the positive angle means left. The
// measurement is clockwise. The range is -90 to 90, but -180 to 180 also works, but the latter is
// not recommended.
pub async fn calculate_coord_by_reference_and_sph_coord(
    origin_x: &f64,
    origin_y: &f64,
    origin_z: &f64,
    elevation_angle: &f64,
    azimuth_angle: &f64,
    distance: &f64,
) -> Result<(f64, f64, f64)> {
    let elevation_angle_sin_cos = elevation_angle.to_radians().sin_cos();
    let elevation_angle_sin = elevation_angle_sin_cos.0;
    let elevation_angle_cos = elevation_angle_sin_cos.1;

    let azimuth_angle_sin_cos = azimuth_angle.to_radians().sin_cos();
    let azimuth_angle_sin = azimuth_angle_sin_cos.0;
    let azimuth_angle_cos = azimuth_angle_sin_cos.1;

    let z_coordinate = elevation_angle_sin * distance;
    let x_coordinate = azimuth_angle_sin * &elevation_angle_cos * distance;
    let y_coordinate = azimuth_angle_cos * &elevation_angle_cos * distance;
    Ok((
        x_coordinate + origin_x,
        y_coordinate + origin_y,
        z_coordinate + origin_z,
    ))
}

#[cfg(test)]
mod tests {
    use crate::coordinate_geometry::calculate_coord_by_reference_and_sph_coord::calculate_coord_by_reference_and_sph_coord;

    #[tokio::test]
    async fn test_1() {
        let cases = vec![
            (100.0, 100.0, 100.0, 60.0, 30.0, 12.0, "simple case"),
            (
                100.0,
                100.0,
                100.0,
                90.0,
                30.0,
                12.0,
                "elevation angle is 90",
            ),
            (
                100.0,
                100.0,
                100.0,
                91.0,
                30.0,
                12.0,
                "elevation angle is 91",
            ),
            (
                100.0,
                100.0,
                100.0,
                180.0,
                30.0,
                12.0,
                "elevation angle is 180",
            ),
            (
                100.0,
                100.0,
                100.0,
                181.0,
                30.0,
                12.0,
                "elevation angle is 181",
            ),
            (
                100.0,
                100.0,
                100.0,
                270.0,
                30.0,
                12.0,
                "elevation angle is 270",
            ),
            (
                100.0,
                100.0,
                100.0,
                271.0,
                30.0,
                12.0,
                "elevation angle is 271",
            ),
            (
                100.0,
                100.0,
                100.0,
                360.0,
                30.0,
                12.0,
                "elevation angle is 360",
            ),
            (
                100.0,
                100.0,
                100.0,
                361.0,
                30.0,
                12.0,
                "elevation angle is 361",
            ),
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
            (
                12364.2342,
                33789.2336,
                34222.229976,
                32.8,
                11.32,
                32.0,
                "big coordinates 1",
            ),
        ];
        for case in cases {
            let origin_x = case.0;
            let origin_y = case.1;
            let origin_z = case.2;
            let elevation_angle = case.3;
            let azimuth_angle = case.4;
            let distance: f64 = case.5;

            let result = calculate_coord_by_reference_and_sph_coord(
                &origin_x,
                &origin_y,
                &origin_z,
                &elevation_angle,
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
            println!(
                "The case: {}, expected: {}, got: {}",
                case.6, distance, calculated_distance_from_result
            );
            assert_eq!(
                distance, rounded_expected_distance,
                "The case: {}, expected: {}, got: {}",
                case.6, distance, calculated_distance_from_result
            );
        }
    }
}
