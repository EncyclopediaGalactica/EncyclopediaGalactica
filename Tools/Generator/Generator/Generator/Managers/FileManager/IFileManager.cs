namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.FileManager;

/// <summary>
///     Filemanager interface
///     <remarks>
///         Provides an interface to deal with all the file related operation during code generation.
///     </remarks>
/// </summary>
public interface IFileManager
{
    /// <summary>
    ///     Deletes the file specified by the path.
    ///     <remarks>
    ///         If the files does not exist no error will be displayed
    ///     </remarks>
    /// </summary>
    /// <param name="pathToFile">path to the file</param>
    void DeleteFile(string pathToFile);

    /// <summary>
    ///     Checks if the directory exists. If not creates one.
    ///     <remarks>
    ///         It creates only the last segment of the path. If further segments are missing it fails.
    ///     </remarks>
    /// </summary>
    /// <param name="pathToFile">path to be checked</param>
    void CheckIfExistsOrCreate(string pathToFile);

    /// <summary>
    ///     Checks if teh given file exists
    /// </summary>
    /// <param name="pathToFile">the path to the file</param>
    /// <returns>bool</returns>
    bool CheckIfFileExist(string pathToFile);

    /// <summary>
    ///     Reads the content of the designated file and stores in memory for further processing
    /// </summary>
    /// <param name="pathToFile">path for the file</param>
    /// <returns>the content of the designated file</returns>
    string ReadAllText(string pathToFile);

    /// <summary>
    ///     Write the provided content into the provided file.
    ///     <remarks>
    ///         This method does not deal with creating the file if it a non-existing one.
    ///         One has to make sure the file exist.
    ///     </remarks>
    /// </summary>
    /// <param name="content">the content</param>
    /// <param name="pathToFile">the file</param>
    void WriteContentIntoFile(string content, string pathToFile);
}