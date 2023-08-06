namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Fact]
    public void ProcessTypename()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTypeNameToken = "originalTypenameToken"
            }
        };
        string typeNamePostfix = "Dto";

        // Act
        _sut.ProcessTypeName(fileInfos, typeNamePostfix);

        // Assert
        fileInfos[0].TypeName.Should().Be("OriginalTypenameTokenDto");
    }

    [Fact]
    public void ProcessTypename_WhenEmptyTypeInfoInput()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>();
        string typeNamePostfix = "Dto";

        // Act
        Action action = () => { _sut.ProcessTypeName(fileInfos, typeNamePostfix); };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void ProcessTypename_WhenNoPostfixProvided()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTypeNameToken = "originalTypenameToken"
            }
        };

        // Act
        _sut.ProcessTypeName(fileInfos, null!);

        // Assert
        fileInfos[0].TypeName.Should().Be("OriginalTypenameToken");
    }
}