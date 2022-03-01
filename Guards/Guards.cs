namespace Guards;

public static class Against
{
    public static class It
    {
        public static class Is
        {
            public static void Null<T>(T val)
            {
                if (val is not null) return;
                string msg = $"The provided object value is null.";
                throw new GuardAgainstException(msg);
            }
        }
    }
}