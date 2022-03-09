namespace Guards;

public static class Guard
{
    public static void NotNull<T>(T val)
    {
        if (val is not null) return;
        string msg = $"The provided object value is null.";
        throw new GuardAgainstException(msg);
    }
}