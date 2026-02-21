use anyhow::Result;

use crate::star_systems::get_coordinates_by_name::get_coordinates_by_name::get_star_system_coordinates_by_name;

use super::types::ExtendGalaxyByAStarSystemInRespectToAStarSystemInput;

// Extend the given galaxy by a star system. In other words, adding a new star system to the
// galagxy. 
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
pub async fn extend_galaxy_by_a_star_system_in_respect_to_a_star_system(
    input: ExtendGalaxyByAStarSystemInRespectToAStarSystemInput,
) -> Result<ExtendGalaxyByAStarSystemInRespectToAStarSystemResult> {
    let reference_star_system_coordinates =
        get_star_system_coordinates_by_name(&input.reference_star_system_name).await?;
    let new_star_system_coordinates = 
}
