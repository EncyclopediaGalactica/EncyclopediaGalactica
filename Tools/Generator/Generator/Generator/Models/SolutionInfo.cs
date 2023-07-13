namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

using Managers.SolutionManager;

public class SolutionInfo
{
    /// <summary>
    ///     Gets or sets the original name value token
    ///     <remarks>
    ///         This value is copied from configuration without any modification
    ///     </remarks>
    /// </summary>
    public string OriginalNameToken { get; set; }

    /// <summary>
    ///     Gets or sets the project infos
    ///     <remarks>
    ///         List of projects within a solution
    ///     </remarks>
    /// </summary>
    public List<ProjectInfo> ProjectInfos { get; set; } = new List<ProjectInfo>();

    /// <summary>
    ///     Gets or sets the original base path token value
    ///     <remarks>
    ///         This value is copied directly from the configuration without any modification
    ///     </remarks>
    /// </summary>
    public string OriginalTargetDirectoryToken { get; set; }

    /// <summary>
    ///     Gets or sets the name value
    ///     <remarks>
    ///         The name of the solution.
    ///     </remarks>
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the base path value
    ///     <remarks>
    ///         The directory where the solution is located
    ///     </remarks>
    /// </summary>
    public string BaseAbsolutePath { get; set; }

    /// <summary>
    ///     Gets or sets the solution file with full path value
    /// </summary>
    public string SolutionFileWithFullPath { get; set; }

    /// <summary>
    ///     Gets or sets the value of solution manager
    ///     <remarks>
    ///         The instance of <see cref="SolutionManager" /> provides convenient methods for solution file
    ///     </remarks>
    /// </summary>
    public ISolutionManager SolutionManager { get; set; }

    /// <summary>
    ///     Gets or sets the value of solution file type
    /// </summary>
    public string SolutionFileType { get; set; }

    /// <summary>
    ///     Gets or sets the solution file with extenstion value
    /// </summary>
    public string? SolutionNameWithFileExtension { get; set; }

    /// <summary>
    ///     Gets or sets the file type for the projects in the solution
    ///     <remarks>
    ///         The default file type for projects in a solution are the following:
    ///         <list type="bullet">
    ///             <item>c-sharp: csproj</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    public string? SolutionProjectFileType { get; set; }
}