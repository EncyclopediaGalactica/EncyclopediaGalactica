namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using Microsoft.Extensions.Logging;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string? CheckIFirstCharSlashAndRemoveIt(string? s1)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrWhiteSpace(s1))
        {
            _logger.LogInformation("{S} is null or empty", nameof(s1));
            return s1;
        }

        if (s1.StartsWith("."))
        {
            return s1.Substring(1);
        }

        return s1;
    }
}