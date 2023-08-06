namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string ToLowerCase(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return String.Empty;
        }

        return s.ToLower();
    }
}