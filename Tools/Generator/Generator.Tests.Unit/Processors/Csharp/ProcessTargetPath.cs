namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("target/path", "dto/base", "dto/add", "target/path/dto/base/dto/add")]
    [InlineData("/target/path", "dto/base", "dto/add", "/target/path/dto/base/dto/add")]
    public void ProcessTargetPath(
        string targetDirectoryToken,
        string dtoBasePathToken,
        string dtoAdditionalPathToken,
        string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTargetDirectoryToken = targetDirectoryToken,
                OriginalDtoProjectBasePathToken = dtoBasePathToken,
                OriginalDtoProjectAdditionalPathToken = dtoAdditionalPathToken
            }
        };

        if (expected[0].ToString() != "/")
        {
            expected = $"{Directory.GetCurrentDirectory()}/{expected}";
        }

        // Act
        _sut.ProcessDtoTargetPath(fileInfos);

        // Assert
        fileInfos[0].AbsoluteTargetPath.Should().Be(expected);
    }

    [Theory]
    [InlineData("target/path", "/dto/base", "dto/add")]
    [InlineData("/target/path", "/dto/base", "dto/add")]
    [InlineData("target/path", "dto/base", "/dto/add")]
    [InlineData("/target/path", "dto/base", "/dto/add")]
    public void Throw_WhenAdditionalPaths_AreAbsolute_InProcessTargetPath(
        string targetDirectoryToken,
        string dtoBasePathToken,
        string dtoAdditionalPathToken)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTargetDirectoryToken = targetDirectoryToken,
                OriginalDtoProjectBasePathToken = dtoBasePathToken,
                OriginalDtoProjectAdditionalPathToken = dtoAdditionalPathToken
            }
        };

        // Act
        Action action = () => { _sut.ProcessDtoTargetPath(fileInfos); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}