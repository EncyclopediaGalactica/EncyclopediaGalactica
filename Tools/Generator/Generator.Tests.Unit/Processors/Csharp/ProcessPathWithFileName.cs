namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("/asd/something", "filename.txt", "/asd/something/filename.txt")]
    [InlineData(" ", "filename.txt", null)]
    [InlineData("", "filename.txt", null)]
    [InlineData(null, "filename.txt", null)]
    [InlineData("/asd/something", "", null)]
    [InlineData("/asd/something", " ", null)]
    [InlineData("/asd/something", null, null)]
    public void ProcessPathWithFileName(
        string targetPath,
        string fileName,
        string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                AbsoluteTargetPath = targetPath,
                FileName = fileName
            }
        };

        // Act
        _sut.ProcessPathWithFileName(fileInfos);

        // Assert
        fileInfos[0].TargetPathWithFileName.Should().Be(expected);
    }
}