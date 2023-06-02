namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit;

using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

public class TestBaseTests : TestBase
{
    public TestBaseTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Theory]
    [InlineData("line", "#TEST_IGNORE", false)]
    [InlineData("lines and lines", "#TEST_IGNORE", false)]
    [InlineData("lines and lines", "#TEST_IGNORE", false)]
    [InlineData("#TEST_IGNORE", "#TEST_IGNORE", true)]
    [InlineData("public class Whatever<T,Y> { #TEST_IGNORE", "#TEST_IGNORE", true)]
    public void ShouldBeIgnoredTests(
        string line,
        string pattern,
        bool expectedResult)
    {
        // Arrange && Act
        bool result = ShouldBeIgnored(line, pattern);

        // Assert
        result.Should().Be(expectedResult);
    }
}