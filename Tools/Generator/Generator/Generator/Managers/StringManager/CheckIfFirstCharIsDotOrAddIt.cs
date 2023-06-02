namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string? CheckIfFirstCharIsDotOrAddIt(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        if (s[0].ToString() != ".")
        {
            return $".{s}";
        }

        return s;
    }
}