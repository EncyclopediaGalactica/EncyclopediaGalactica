namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using System.Text;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string Concat(string s1, string? s2)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(s1) && !string.IsNullOrWhiteSpace(s1))
        {
            builder.Append(s1.Trim());
        }

        if (!string.IsNullOrEmpty(s2) && !string.IsNullOrWhiteSpace(s2))
        {
            builder.Append(s2.Trim());
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public string Concat(string s1, string s2, string s3)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(s1) && !string.IsNullOrWhiteSpace(s1))
        {
            builder.Append(s1.Trim());
        }

        if (!string.IsNullOrEmpty(s2) && !string.IsNullOrWhiteSpace(s2))
        {
            builder.Append(s2.Trim());
        }

        if (!string.IsNullOrEmpty(s3) && !string.IsNullOrWhiteSpace(s3))
        {
            builder.Append(s3.Trim());
        }

        return builder.ToString();
    }
}