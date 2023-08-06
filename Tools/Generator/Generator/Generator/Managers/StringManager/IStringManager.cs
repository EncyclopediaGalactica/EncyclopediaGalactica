namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

/// <summary>
///     String manager interface
///     <remarks>
///         It provides methods for dealing with all strings related operations during code generation
///     </remarks>
/// </summary>
public interface IStringManager
{
    /// <summary>
    ///     Concatenates two string fragments
    /// </summary>
    /// <param name="s1">string 1</param>
    /// <param name="s2">string 2</param>
    /// <returns>the new string</returns>
    string Concat(string s1, string? s2);

    /// <summary>
    ///     Concatenates three string fragments
    /// </summary>
    /// <param name="s1">string 1</param>
    /// <param name="s2">string 2</param>
    /// <param name="s3">string 3</param>
    /// <returns>the new string</returns>
    string Concat(string s1, string s2, string s3);

    /// <summary>
    ///     Makes the first character of the provided string uppercase
    ///     <remarks>
    ///         If the input is null, empty or whitespace the method returns these
    ///     </remarks>
    /// </summary>
    /// <param name="s">string</param>
    /// <returns>the new string</returns>
    string MakeFirstCharUpperCase(string? s);

    /// <summary>
    ///     Concatenates two string as they were C# namespaces.
    ///     <remarks>
    ///         The two string will be separated by a dot (.).
    ///         If both of the strings are null, empty or whitespace the method returns string.empty
    ///     </remarks>
    /// </summary>
    /// <param name="s1">namespace 1</param>
    /// <param name="s2">namespace 2</param>
    /// <returns>Concatenated namespaces</returns>
    string ConcatCsharpNamespaceTokens(string? s1, string? s2);

    /// <summary>
    ///     Makes all capital letter in the provided string uppercase
    ///     <remarks>
    ///         C# namespaces are in this format
    ///         If the input string is null, empty or whitespace the method returns string.empty
    ///     </remarks>
    /// </summary>
    /// <param name="s">string</param>
    /// <returns>modified string</returns>
    string? MakeUppercaseTheCharAfterTheDot(string? s);

    /// <summary>
    ///     Makes the input string lowercase
    ///     <remarks>
    ///         If the input string is null, empty or whitespace string empty sill be returned
    ///     </remarks>
    /// </summary>
    /// <param name="s">string</param>
    /// <returns>modified string</returns>
    string ToLowerCase(string? s);

    /// <summary>
    ///     Checks if the first character of the provided string is "/" and throws if it is.
    /// </summary>
    /// <param name="s">string</param>
    void CheckIfFirstCharIsSlashAndThrow(string? s);

    /// <summary>
    ///     Checks if the provided string's last character is a "/", if so removes it
    ///     <remarks>
    ///         If the input string null, empty or whitespace string.empty will return
    ///     </remarks>
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>modified string</returns>
    string CheckIfLastCharSlashAndRemoveIt(string? s);

    /// <summary>
    ///     Checks if the first char of the provided string is a "/"
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>bool</returns>
    bool IsFirstCharIsASlash(string? s);

    /// <summary>
    ///     Transforms a snake_case string to PascalCase
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>modified string</returns>
    string? MakeSnakeCaseToPascalCase(string? s);

    /// <summary>
    ///     Checks if the first character of the provided string is a dot ".", if not adds it
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns>modified string</returns>
    string? CheckIfFirstCharIsDotOrAddIt(string? s);

    /// <summary>
    ///     Concats two string fragments following c-sharp project naming rules
    /// </summary>
    /// <param name="s1">String fragment</param>
    /// <param name="s2">String fragment</param>
    /// <returns>Project name</returns>
    string? ConcatForCSharpProjectName(string? s1, string? s2);

    /// <summary>
    ///     Checks if the first character of the provided string is a dot and removes it.
    /// </summary>
    /// <param name="s1">String to be checked</param>
    /// <returns>The modified string</returns>
    string? CheckIfFirstCharIsDotAndRemoveIt(string? s1);

    /// <summary>
    ///     Checks if the last character of the provided string is a dot and removes it.
    /// </summary>
    /// <param name="s1">String to be checked</param>
    /// <returns>The modified string</returns>
    string? CheckIfLastCharIsDotAndRemoveIt(string? s1);

    /// <summary>
    ///     Checks if the first and the last characters are dot and removes them.
    ///     <remarks>
    ///         It removes the dots even if there the first char is a dot but not the last one, and vica versa.
    ///     </remarks>
    /// </summary>
    /// <param name="s1"></param>
    /// <returns></returns>
    string? CheckIfFirstAndLastCharDotAndRemoveIt(string? s1);

    /// <summary>
    ///     Concatenates two directory path segments
    ///     <remarks>
    ///         <p>The first segment can be absolute or relative path</p>
    ///         <p>
    ///             The second segment also can be absolute or relative path, but if it is absolute a log message on the
    ///             console
    ///             will be produced
    ///         </p>
    ///     </remarks>
    /// </summary>
    /// <param name="s1">First directory segment</param>
    /// <param name="s2">Second directory segment</param>
    /// <returns>Concatenated directory path</returns>
    string ConcatDirectorySegments(string s1, string? s2);

    /// <summary>
    ///     removes the first character of a string if it is a dot
    /// </summary>
    /// <param name="s1">the string</param>
    /// <returns>modified string</returns>
    string? CheckIFirstCharSlashAndRemoveIt(string? s1);
}