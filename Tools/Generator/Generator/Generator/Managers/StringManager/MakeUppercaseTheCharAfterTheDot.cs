namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.StringManager;

using System.Text;

public partial class StringManagerImpl
{
    /// <inheritdoc />
    public string MakeUppercaseTheCharAfterTheDot(string? s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return string.Empty;
        }

        StringBuilder builder = new StringBuilder();
        bool shouldBeCapital = false;
        for (int i = 0; i < s.Length; i++)
        {
            if (i == 0)
            {
                builder.Append(s[i].ToString().ToUpper());
                continue;
            }

            if (shouldBeCapital)
            {
                builder.Append(s[i].ToString().ToUpper());
                shouldBeCapital = false;
                continue;
            }

            if (s[i].ToString() == ".")
            {
                shouldBeCapital = true;
                builder.Append(s[i]);
            }
            else
            {
                builder.Append(s[i]);
            }
        }

        return builder.ToString();
    }
}