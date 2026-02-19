#[derive(Debug, Clone)]
pub struct ExtendGalaxyByAStarSystemInRespectToAStarSystemInput {
    elevation_angle: f64,
    direction_angle: f64,
    distance: f64,
    reference_star_system_name: i64,
}

impl ExtendGalaxyByAStarSystemInRespectToAStarSystemInput {
    pub fn new(
        elevation_angle: f64,
        direction_angle: f64,
        distance: f64,
        reference_star_system_name: i64,
    ) -> Self {
        Self {
            elevation_angle,
            direction_angle,
            distance,
            reference_star_system_name,
        }
    }
}
