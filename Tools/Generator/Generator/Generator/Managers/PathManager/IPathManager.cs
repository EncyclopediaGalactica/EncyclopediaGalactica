namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

/// <summary>
///     Path manager interfaces
///     <remarks>
///         Deals with all the path related tasks during code generation
///     </remarks>
/// </summary>
public interface IPathManager
{
    /// <summary>
    ///     Takes the provided strings and concatenates them into a valid path.
    ///     <remarks>
    ///         <list type="bullet">
    ///             <item>ensures slash between string fragment</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    /// <param name="path1">path fragment</param>
    /// <param name="path2">path fragment</param>
    /// <returns>Path constructed from the input path fragments</returns>
    string BuildPathString(
        string path1,
        string path2);

    /// <summary>
    ///     Takes the provided strings and concatenates them into a valid path.
    ///     <remarks>
    ///         <list type="bullet">
    ///             <item>ensures slash between string fragment</item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    /// <param name="path1">path fragment</param>
    /// <param name="path2">path fragment</param>
    /// <param name="path3">path fragment</param>
    /// <returns>Path constructed from the input path fragments</returns>
    string BuildPathString(
        string path1,
        string path2,
        string path3);

    /// <summary>
    ///     Builds a path from path and filename input parameters
    ///     <remarks>
    ///         It ensures that there is a slash between path and filename.
    ///     </remarks>
    /// </summary>
    /// <param name="path">the path to the directory where the file is placed</param>
    /// <param name="fileName">the filename</param>
    /// <returns>path to the file</returns>
    string BuildPathStringWithFilename(string path, string fileName);

    /// <summary>
    ///     Checks if the provided path exists. If not it creates it.
    /// </summary>
    /// <param name="path">the path</param>
    void CheckOrCreatePath(string path);

    /// <summary>
    ///     Provides the current directory path.
    /// </summary>
    /// <returns>Current directory path</returns>
    string GetCurrentDirectory();

    /// <summary>
    ///     Checks if given path exists
    /// </summary>
    /// <param name="path">the path to the directory</param>
    /// <returns>bool</returns>
    bool IsPathExists(string path);

    /// <summary>
    ///     Creates the designated path
    /// </summary>
    /// <param name="path">the path</param>
    void CreatePath(string path);

    /// <summary>
    ///     Deletes the designated path
    /// </summary>
    /// <param name="path">the path</param>
    /// <param name="recursive">should the deletion be recursive or not</param>
    void DeletePath(string path, bool recursive = false);

    /// <summary>
    ///     Checks if the given path has "/" as last character. If so, it removes it and returns this new version.
    /// </summary>
    /// <param name="path">the path</param>
    /// <returns>string</returns>
    string CheckAndRemoveEndSlash(string path);

    /// <summary>
    ///     It checks if a path is absolute or relative. If relative it makes an absolute one. It uses the directory path where
    ///     the code runs and creates an absolute path
    /// </summary>
    /// <param name="path">the path</param>
    /// <returns>the new path</returns>
    string CheckIfPathAbsoluteOrMakeItOne(string path);
}