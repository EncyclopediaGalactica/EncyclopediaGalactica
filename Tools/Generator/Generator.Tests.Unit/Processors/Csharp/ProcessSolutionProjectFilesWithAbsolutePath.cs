namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData(null, "asdasd")]
    [InlineData("", "asdasd")]
    [InlineData("asdad", "")]
    [InlineData("asdad", null)]
    public void ProcessSolutionProjectFilesWithAbsolutePath_Throw_WhenInvalidInput(
        string basePath,
        string solutionProjectFileWithType)
    {
        // Arrange
        SolutionInfo solutionInfo = new SolutionInfo
        {
            ProjectInfos = new List<ProjectInfo>
            {
                new ProjectInfo
                {
                    BasePath = basePath,
                    SolutionProjectFileWithType = solutionProjectFileWithType
                }
            }
        };

        // Act
        Action action = () => { _sut.ProcessSolutionProjectFilesWithAbsolutePath(solutionInfo); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("aaaa", "ssss", "aaaa/ssss")]
    [InlineData("a", "d", "a/d")]
    public void ProcessSolutionProjectFilesWithAbsolutePath(
        string basePath,
        string solutionProjectFileWithType,
        string expectedResult)
    {
        // Arrange
        SolutionInfo solutionInfo = new SolutionInfo
        {
            ProjectInfos = new List<ProjectInfo>
            {
                new ProjectInfo
                {
                    BasePath = basePath,
                    SolutionProjectFileWithType = solutionProjectFileWithType
                }
            }
        };

        // Act
        _sut.ProcessSolutionProjectFilesWithAbsolutePath(solutionInfo);

        // Assert
        solutionInfo.ProjectInfos.First().ProjectFileWithAbsolutePath
            .Should().Be(expectedResult);
    }
}