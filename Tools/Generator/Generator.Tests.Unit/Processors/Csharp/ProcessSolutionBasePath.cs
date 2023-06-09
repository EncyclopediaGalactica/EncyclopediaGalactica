namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("asd")]
    [InlineData("asd/asd")]
    [InlineData("/asd/asd")]
    public void ProcessSolutionBasePath(string input)
    {
        // Arrange
        SolutionInfo solutionInfo = new SolutionInfo
        {
            OriginalTargetDirectoryToken = input
        };
        string expected;
        if (input.First().ToString() == "/")
        {
            expected = input;
        }
        else
        {
            expected = $"{Directory.GetCurrentDirectory()}/{input}";
        }

        // Act
        _sut.ProcessSolutionBasePath(solutionInfo);

        // Assert
        solutionInfo.BaseAbsolutePath.Should().Be(expected);
    }
}