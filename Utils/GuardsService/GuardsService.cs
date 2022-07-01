namespace EncyclopediaGalactica.Utils.GuardsService;

using Exceptions;
using Interfaces;

public class GuardsService : IGuardsService
{
    const string Msg = "Error happened while validating. For further information see inner exception.";

    /// <inheritdoc />
    public void NotNull<T>(T val)
    {
        if (val is not null) return;
        throw new GuardsServiceValueShouldNoBeNullException($"The provided object value is null.");
    }

    /// <inheritdoc />
    public void IsNotEqual(long providedValue, long comparedTo)
    {
        if (providedValue == comparedTo)
        {
            string msg;
            msg = $"The provided value {providedValue} cannot be equal to: {comparedTo}";
            throw new GuardsServiceValueShouldNotBeEqualToException(msg);
        }
    }
}