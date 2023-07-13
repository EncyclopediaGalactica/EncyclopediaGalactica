namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Structure;

using cSharp;

public interface IStructureDescriptor
{
    /// <summary>
    ///     Returns the Solution Project Filetype default value
    /// </summary>
    string SolutionProjectDefaultFileType { get; }

    /// <summary>
    ///     Returns the Solution Filetype default value
    /// </summary>
    string SolutionDefaultFileType { get; }

    /// <summary>
    ///     Returns he list of projects. This list is the base of all operations during generating the code.
    /// </summary>
    /// <returns>List of projects</returns>
    List<string> GetProjects();

    /// <summary>
    ///     Returns the naming pattern for the given project slot name
    /// </summary>
    /// <param name="slotName">the name of the slot</param>
    /// <returns>the naming pattern for the slot</returns>
    string GetNamingPatternBySlot(string slotName);

    /// <summary>
    ///     Returns the <see cref="ProjectDescriptor" /> of the designated project slot
    /// </summary>
    /// <param name="itemSlot">the requested slot information</param>
    /// <returns>Instance of <see cref="ProjectDescriptor" /></returns>
    ProjectDescriptor GetProject(string itemSlot);
}