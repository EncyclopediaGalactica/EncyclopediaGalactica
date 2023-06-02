namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("filenameToken", "Dto", ".cs", "FilenameTokenDto.cs")]
    [InlineData("FilenameToken", "Dto", ".cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "dto", ".cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", "cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", ".Cs", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", ".CS", "FilenameTokenDto.cs")]
    [InlineData("filenameToken", "Dto", "CS", "FilenameTokenDto.cs")]
    public void ProcessFileName(
        string typenameToken,
        string filenamePostfix,
        string fileType,
        string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTypeNameToken = typenameToken
            }
        };
        string typenamePostfix = filenamePostfix;
        string filetype = fileType;

        // Act
        _sut.ProcessFileName(fileInfos, typenamePostfix, filetype);

        // Assert
        fileInfos[0].FileName.Should().Be(expected);
    }

    [Fact]
    public void ProcessFileName_WhenFileInfosListIsEmpty()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>();
        string typenamePostfix = "Dto";
        string filetype = ".cs";

        // Act
        Action action = () => { _sut.ProcessFileName(fileInfos, typenamePostfix, filetype); };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ProcessFileName_ThrowWhenFilenamePostfix_IsNotProvided(string filetype)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTypeNameToken = "originalTypenameToken"
            }
        };
        string typenamePostfix = "Dto";

        // Act
        Action action = () => { _sut.ProcessFileName(fileInfos, typenamePostfix, filetype!); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}