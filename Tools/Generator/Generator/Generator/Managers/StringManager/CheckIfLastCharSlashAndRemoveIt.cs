namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

public partial class StringManagerImpl
{
    public string CheckIfLastCharSlashAndRemoveIt(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return String.Empty;
        }

        if (s[^1].ToString() == "/")
        {
            return s[..^1];
        }

        return s;
    }
}