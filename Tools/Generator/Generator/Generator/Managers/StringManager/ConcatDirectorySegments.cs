namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using System.Text;
using Microsoft.Extensions.Logging;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string ConcatDirectorySegments(string? s1, string? s2)
    {
        if ((string.IsNullOrEmpty(s1) || string.IsNullOrWhiteSpace(s1))
            && (string.IsNullOrEmpty(s2) || string.IsNullOrWhiteSpace(s2)))
        {
            _logger.LogInformation(
                "{S1} and {S2} are empty or null!",
                nameof(s1),
                nameof(s2));
            return string.Empty;
        }

        if ((!string.IsNullOrEmpty(s1) || !string.IsNullOrWhiteSpace(s1))
            && (string.IsNullOrEmpty(s2) || string.IsNullOrWhiteSpace(s2)))
        {
            _logger.LogInformation("{S2} is null or empty", nameof(s2));
            return s1;
        }

        if ((string.IsNullOrEmpty(s1) || string.IsNullOrWhiteSpace(s1))
            && (!string.IsNullOrEmpty(s2) || !string.IsNullOrWhiteSpace(s2)))
        {
            _logger.LogInformation("{S1} is null or empty", nameof(s1));
            return s2;
        }

        if (s2.StartsWith("/"))
        {
            _logger.LogInformation(
                "{S2} first char is a slash (/), probably absolute path",
                nameof(s2)
            );
        }

        return new StringBuilder()
            .Append(CheckIfLastCharSlashAndRemoveIt(s1))
            .Append("/")
            .Append(CheckIFirstCharSlashAndRemoveIt(s2))
            .ToString();
    }
}