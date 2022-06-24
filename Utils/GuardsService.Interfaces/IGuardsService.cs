namespace EncyclopediaGalactica.Utils.GuardsService.Interfaces;

using Exceptions;

public interface IGuardsService
{
    /// <summary>
    ///     Checks if the provided value is null. If so, it throws.
    /// </summary>
    /// <param name="val">The value to be checked</param>
    /// <typeparam name="T">Type of the value</typeparam>
    /// <exception cref="GuardsServiceValueShouldNoBeNullException">
    ///     When the provided value is null.
    /// </exception>
    void NotNull<T>(T val);

    /// <summary>
    ///     Checks if the two provided long values are equal.
    ///     If so, then it throws exception.
    /// </summary>
    /// <param name="providedValue">Provided value</param>
    /// <param name="comparedTo">To be compared to</param>
    /// <exception cref="GuardsServiceValueShouldNotBeEqualToException">
    ///     When the provided value equals to the compared to value.
    /// </exception>
    void IsNotEqual(long providedValue, long comparedTo);
}