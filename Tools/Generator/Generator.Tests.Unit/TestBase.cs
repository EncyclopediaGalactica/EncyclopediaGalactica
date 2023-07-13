namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit;

using FluentAssertions;
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

    protected void CompareFileLineByLine(string pathToReference, string generatedFile)
    {
        List<string> reference = File.ReadLines(pathToReference).ToList();
        List<string> generated = File.ReadLines(generatedFile).ToList();
        for (int i = 0; i < reference.Count(); i++)
        {
            if (ShouldBeIgnored(reference[i], "#TEST_IGNORE"))
            {
                continue;
            }

            generated[i].Should().Be(reference[i],
                $"There is a difference in line {i}. \n" +
                $"Reference file: {pathToReference} \n" +
                $"Generated file: {generated}");
        }
    }
}