namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Structure.cSharp;

public class ProjectDescriptor
{
    /// <summary>
    ///     Gets or sets the naming pattern value
    ///     <remarks>
    ///         This naming pattern will be used to create the project name. It will be concatenated to solution name, like
    ///         follows: {SolutionName}.{Pattern}
    ///     </remarks>
    /// </summary>
    public string NamingPattern { get; set; }

    /// <summary>
    ///     Gets or sets the value of dependencies
    ///     <remarks>
    ///         The dependency marks those projects by their slot name which needed in order to be able to compile the
    ///         project
    ///     </remarks>
    /// </summary>
    public List<string> DependenciesBySlotName { get; set; }
}