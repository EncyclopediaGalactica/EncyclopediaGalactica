namespace EncyclopediaGalactica.Utils.GuardsService;

public interface IGuardsService
{
    void NotNull<T>(T val);
    void IsNotEqual(long providedValue, long comparedTo);
}