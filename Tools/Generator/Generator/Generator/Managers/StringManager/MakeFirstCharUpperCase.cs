namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using Microsoft.Extensions.Logging;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string MakeFirstCharUpperCase(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            LoggerExtensions.LogInformation(_logger, "Input string is null, empty or whitespace");
            return s;
        }

        return $"{s[0].ToString().ToUpper()}{s.Substring(1, s.Length - 1)}";
    }
}