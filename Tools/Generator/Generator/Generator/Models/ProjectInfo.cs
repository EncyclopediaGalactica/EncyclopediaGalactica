namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

using Managers.SolutionProjectManager;

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
}