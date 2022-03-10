namespace Guards;

public static class Guard
{
    public static void NotNull<T>(T val)
    {
        if (val is not null) return;
        string msg = $"The provided object value is null.";
        throw new GuardValueShouldNoBeNullException(msg);
    }

    public static void IsNotEqual(long providedValue, long comparedTo)
    {
        if (providedValue == comparedTo)
        {
            string msg;
            msg = $"The provided value {providedValue} cannot be equal to: {comparedTo}";
            throw new GuardValueShouldNotBeEqualToException(msg);
        }
    }
}