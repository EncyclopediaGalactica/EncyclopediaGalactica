namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit;

using Xunit.Abstractions;

public class TestBase
{
    protected readonly string BasePath = Directory.GetCurrentDirectory();
    protected readonly ITestOutputHelper OutputHelper;

    public TestBase(ITestOutputHelper outputHelper)
    {
        ArgumentNullException.ThrowIfNull(outputHelper);
        OutputHelper = outputHelper;
    }

    /// <summary>
    ///     If the pattern can be found in the line it returns true.
    ///     This method is used in generated code testing due to that we have value changes every generation. One of them is
    ///     when the code was generated.
    /// </summary>
    /// <param name="line">the actual line</param>
    /// <param name="pattern">the pattern </param>
    /// <returns>bool</returns>
    protected bool ShouldBeIgnored(string line, string pattern)
    {
        if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(pattern))
        {
            return false;
        }

        if (line.Contains(pattern))
        {
            return true;
        }

        return false;
    }
}