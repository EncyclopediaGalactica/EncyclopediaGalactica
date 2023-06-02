namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string? CheckIfFirstAndLastCharDotAndRemoveIt(string? s1)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrWhiteSpace(s1))
        {
            return s1;
        }

        s1 = CheckIfFirstCharIsDotAndRemoveIt(s1);
        s1 = CheckIfLastCharSlashAndRemoveIt(s1);

        return s1;
    }
}