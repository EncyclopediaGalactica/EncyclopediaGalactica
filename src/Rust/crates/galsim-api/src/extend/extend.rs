use anyhow::Result;

use crate::star_systems::get_coordinates_by_name::get_coordinates_by_name::get_star_system_coordinates_by_name;

use super::types::ExtendGalaxyByAStarSystemInRespectToAStarSystemInput;

// Extend the given galaxy by a star system.
// The viewpoint of the extend operation is the center of the given galaxy.
// The "in respect to" means a single star system.
//
//  The parameters are in the x, y and d directions, but not all of the means coordinates.
//  Defining a star system by coordinates would be a pain in the rear and would not help to build a
//  galaxy map. Even though the coordinates are the ultimate position of a star system they do not
//  show information in an explicit, human understandable way. The case here is the same as the
//  orbital elements versus the coordinates of the orbit.
//
//  In this endpoint we use the galaxy center and another star system as a reference.
//  The galaxy center reference in coordination with the star system reference tells us whether the
//  The star system reference and the angles tell us whether the new star system is left or right
//  and up or down if we look at it from the galaxy center.
//  The next parameter is the distance of the new star system from the reference star system.
//  I selected this measure becayse this endpoint is about to build a galaxy map that fits for my
//  imagination. My imagination is what is the distance between star systems and not the distance
//  from the galaxy center. The latter is important but doesn't tell us anything. Moreover, it can
//  be calculated.
//
// The meaning of the parameters from the galaxy center point of view is the following:
// - 'direction angle':
//   - farther and right is the 0 - 90 degrees
//   - farther and left is the 90 - 180 degrees
//   - closer and left is the 180 - 270 degrees
//   - closer and right is the 270 - 360 degrees
// - 'elevation angle':
//   - farther and up is the 0 - 90 degrees
//   - closer and up is the 90 - 180 degrees
//   - closer and down is the 180 - 270 degrees
//   - farther and down is the 270 - 360 degrees
// - 'd' is distance from the "in respect to" star system
//
pub async fn extend_galaxy_by_a_star_system_in_respect_to_a_star_system(
    input: ExtendGalaxyByAStarSystemInRespectToAStarSystemInput,
) -> Result<ExtendGalaxyByAStarSystemInRespectToAStarSystemResult> {
    let reference_star_system_coordinates =
        get_star_system_coordinates_by_name(&input.reference_star_system_name).await?;
    let new_star_system_coordinates = 
}
