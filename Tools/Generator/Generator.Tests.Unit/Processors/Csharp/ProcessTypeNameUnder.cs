namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Fact]
    public void ProcessTypeNameUnderTest()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTypeNameToken = "someType"
            }
        };
        string dtoNamePostfix = "Dto";

        // Act
        _sut.ProcessTypeNameUnderTest(fileInfos, dtoNamePostfix);

        // Assert
        fileInfos[0].TypeNameUnderTest.Should().Be("SomeTypeDto");
    }

    [Fact]
    public void ProcessTypeNameUnderTest_WhenEmptyTypeInfoInput()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>();
        string dtoNamePostfix = "Dto";

        // Act
        Action action = () => { _sut.ProcessTypeNameUnderTest(fileInfos, dtoNamePostfix); };

        // Assert
        action.Should().NotThrow();
    }
}