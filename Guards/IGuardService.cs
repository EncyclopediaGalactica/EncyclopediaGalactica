namespace Guards;

public interface IGuardService
{
    void NotNull<T>(T val);
    void IsNotEqual(long providedValue, long comparedTo);
}