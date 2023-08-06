namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Models;

using Managers.SolutionManager;

/// <summary>
///     <p>SolutionInfo Type</p>
///     <p>
///         This object stores all the Solution related information used either during generation or created during
///         generations.
///     </p>
///     <p>
///         Solution is the highest level in the generator directory based data structure. Every piece of code is under a
///         solution. In case of C# there is even a solution file.
///     </p>
/// </summary>
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
    public string OriginalBasePathToken { get; set; }

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
}