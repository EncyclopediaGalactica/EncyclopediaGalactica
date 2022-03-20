namespace Guards;

public class GuardService : IGuardService
{
    const string Msg = "Error happened while validating. For further information see inner exception.";

    public void NotNull<T>(T val)
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

    public void IsNotEqual(long providedValue, long comparedTo)
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