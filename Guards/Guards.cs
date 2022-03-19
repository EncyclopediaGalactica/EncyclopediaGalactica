namespace Guards;

public static class Guard
{
    const string Msg = "Error happened while validating. For further information see inner exception.";

    public static void NotNull<T>(T val)
    {
        try
        {
            if (val is not null) return;
            throw new GuardValueShouldNoBeNullException($"The provided object value is null.");
        }
        catch (Exception e)
        {
            throw new GuardException(Msg, e);
        }
    }

    public static void IsNotEqual(long providedValue, long comparedTo)
    {
        try
        {
            if (providedValue == comparedTo)
            {
                string msg;
                msg = $"The provided value {providedValue} cannot be equal to: {comparedTo}";
                throw new GuardValueShouldNotBeEqualToException(msg);
            }
        }
        catch (Exception e)
        {
            throw new GuardException(Msg, e);
        }
    }
}