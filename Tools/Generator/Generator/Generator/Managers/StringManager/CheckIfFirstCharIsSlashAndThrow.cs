namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public void CheckIfFirstCharIsSlashAndThrow(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return;
        }

        if (s[0].ToString() == "/")
        {
            throw new ArgumentException("First character of string cannot be /");
        }
    }
}