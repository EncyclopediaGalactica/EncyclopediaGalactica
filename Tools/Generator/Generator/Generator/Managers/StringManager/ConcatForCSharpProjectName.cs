namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using System.Text;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string? ConcatForCSharpProjectName(string? s1, string? s2)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrWhiteSpace(s1))
        {
            return string.Empty;
        }

        if ((string.IsNullOrEmpty(s2) || string.IsNullOrWhiteSpace(s2))
            && (!string.IsNullOrEmpty(s1) || !string.IsNullOrWhiteSpace(s1)))
        {
            return s1;
        }

        if ((string.IsNullOrEmpty(s2) || string.IsNullOrWhiteSpace(s2))
            && (string.IsNullOrEmpty(s1) || string.IsNullOrWhiteSpace(s1)))
        {
            return string.Empty;
        }

        return new StringBuilder()
            .Append(CheckIfFirstAndLastCharDotAndRemoveIt(s1.Trim()))
            .Append(".")
            .Append(CheckIfFirstAndLastCharDotAndRemoveIt(s2.Trim()))
            .ToString();
    }
}