namespace EncyclopediaGalactica.Utils.GuardsService;

public class GuardsService : IGuardsService
{
    const string Msg = "Error happened while validating. For further information see inner exception.";

    public void NotNull<T>(T val)
    {
        try
        {
            if (val is not null) return;
            throw new GuardsServiceValueShouldNoBeNullException($"The provided object value is null.");
        }
        catch (Exception e)
        {
            throw new GuardsServiceException(Msg, e);
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
                throw new GuardsServiceValueShouldNotBeEqualToException(msg);
            }
        }
        catch (Exception e)
        {
            throw new GuardsServiceException(Msg, e);
        }
    }
}