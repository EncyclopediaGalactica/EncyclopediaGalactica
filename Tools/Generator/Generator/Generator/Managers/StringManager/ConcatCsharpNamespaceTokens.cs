namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using System.Text;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string ConcatCsharpNamespaceTokens(string? s1, string? s2)
    {
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(s1) && !string.IsNullOrWhiteSpace(s1))
        {
            if (s1[0].ToString() == ".")
            {
                s1 = s1[1..];
            }

            if (s1[^1].ToString() == ".")
            {
                builder.Append(s1.Substring(0, s1.Length - 1));
            }
            else
            {
                builder.Append(s1);
            }
        }

        if (!string.IsNullOrEmpty(s2) && !string.IsNullOrWhiteSpace(s2))
        {
            if (s2[0].ToString() == ".")
            {
                s2 = s2[1..];
            }

            if (s2[^1].ToString() == ".")
            {
                if (builder.Length > 0)
                {
                    builder.Append(".").Append(s2[..^1]);
                }
                else
                {
                    builder.Append(s2[..^1]);
                }
            }
            else
            {
                if (builder.Length > 0)
                {
                    builder.Append(".").Append(s2);
                }
                else
                {
                    builder.Append(s2);
                }
            }
        }

        return builder.ToString();
    }
}