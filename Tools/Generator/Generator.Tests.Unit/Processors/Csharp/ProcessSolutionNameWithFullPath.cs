namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    // [Theory]
    [InlineData("Asd.sln", "/asd/asd", "/asd/asd/Asd.sln")]
    [InlineData("", "/asd/asd", null)]
    [InlineData(null, "/asd/asd", null)]
    [InlineData(" ", "/asd/asd", null)]
    [InlineData("Asd", null, null)]
    [InlineData("Asd", "", null)]
    [InlineData("Asd", " ", null)]
    public void ProcessSolutionNameWithFullPath(string name, string path, string expected)
    {
        // Arrange
        SolutionInfo solutionInfo = new SolutionInfo
        {
            SolutionNameWithFileExtension = name,
            BaseAbsolutePath = path
        };

        // Act
        _sut.ProcessSolutionNameWithFullPath(solutionInfo);

        // Assert
        solutionInfo.SolutionFileWithFullPath.Should().Be(expected);
    }
}