namespace EncyclopediaGalactica.Utils.Guards;

public static class Guards
{
    public static void StringIsNotNullOrEmptyOrWhitespace(string val)
    {
        if (!string.IsNullOrEmpty(val) && !string.IsNullOrEmpty(val)) return;
        string msg = "Provided string is null, empty or whitespace.";
        throw new GuardsException(msg);
    }

    public static void IsNotNull(object? o)
    {
        if (o is null)
            throw new GuardsException("Object cannot be null.");
    }
}