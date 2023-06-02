namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public bool IsFirstCharIsASlash(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        if (s[0].ToString() == "/")
        {
            return true;
        }

        return false;
    }
}