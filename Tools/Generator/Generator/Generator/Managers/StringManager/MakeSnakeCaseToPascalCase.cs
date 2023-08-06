namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using System.Text;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string? MakeSnakeCaseToPascalCase(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        bool makeNextCharCapital = false;
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (i == 0 && s[i].ToString() != "_")
            {
                builder.Append(s[0].ToString().ToUpper());
                continue;
            }

            if (s[i].ToString() == "_")
            {
                makeNextCharCapital = true;
                continue;
            }

            if (makeNextCharCapital)
            {
                builder.Append(s[i].ToString().ToUpper());
                makeNextCharCapital = false;
                continue;
            }

            builder.Append(s[i]);
        }

        return builder.ToString();
    }
}