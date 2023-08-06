namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("asd", "Asd")]
    [InlineData("asd.whatever", "Asd.whatever")]
    public void ProcessSolutionName(string input, string expected)
    {
        // Arrange
        SolutionInfo solutionInfo = new SolutionInfo
        {
            OriginalNameToken = input
        };

        // Act
        _sut.ProcessSolutionName(solutionInfo);

        // Assert
        solutionInfo.Name.Should().Be(expected);
    }
}