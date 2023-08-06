namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

using Managers.SolutionProjectManager;

/// <summary>
///     <p>ProjectInfo Type</p>
///     <p>
///         ProjectInfo Type contains all the information related to a directory in the solution structure.
///     </p>
///     <p>
///         In the generator structure a directory is focusing on a single thing like containing Dtos, or Dto
///         tests, etc. Every project has its own name, purpose and specific set of information and configuration value
///         related transformations.
///     </p>
/// </summary>
public class ProjectInfo
{
    /// <summary>
    ///     Gets or sets the name value
    ///     <remarks>
    ///         The name of the project
    ///     </remarks>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the base path value
    ///     <remarks>
    ///         The base path of the project. In general this path is the solution path and the directory for the project
    ///     </remarks>
    /// </summary>
    public string BasePath { get; set; }

    /// <summary>
    ///     Gets or sets the project file with full path value
    ///     <remarks>
    ///         The full path with the project file and file extension
    ///     </remarks>
    /// </summary>
    public string ProjectFileWithFullPath { get; set; }

    /// <summary>
    ///     Gets or sets the value of slot
    ///     <remarks>
    ///         The generator has information about what projects (including their types and some other properties) will be
    ///         generated.
    ///         The slot is the connection between the structure descriptor and generation information.
    ///     </remarks>
    /// </summary>
    public string? Slot { get; set; }

    /// <summary>
    ///     Gets or sets the value of the solution project manager.
    ///     <remarks>
    ///         this is an instance of <see cref="SolutionProjectManager" /> providing convenient methods to manipulate
    ///         the given csproj file
    ///     </remarks>
    /// </summary>
    public SolutionProjectManager SolutionProjectManager { get; set; }

    /// <summary>
    ///     Gets or sets the path from solution base path string
    ///     <remarks>
    ///         This value is used in referencing projects
    ///     </remarks>
    /// </summary>
    public string PathFromSolutionBase { get; set; }

    /// <summary>
    ///     Gets or sets the file type of a project file in the solution
    ///     <remarks>
    ///         In case of c-sharp it is <b>csproj</b>
    ///     </remarks>
    /// </summary>
    public string SolutionProjectFileType { get; set; }

    /// <summary>
    ///     Gets or sets the solution project file name with filetype extension value
    /// </summary>
    public string SolutionProjectFileWithType { get; set; }

    /// <summary>
    ///     Gets or sets the TypeInfo value
    ///     <remarks>
    ///         <p>The <see cref="TypeInfo" /> describes all the properties of a type and file itself</p>
    ///     </remarks>
    /// </summary>
    public List<TypeInfo> TypeInfos { get; set; }
}