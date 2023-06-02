namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> AddDtoAdditionalPathToTypeInfoData = new List<object[]>
    {
        new[]
        {
            new CodeGeneratorConfiguration()
        },
        new[]
        {
            new CodeGeneratorConfiguration
            {
                DtoProjectAdditionalPath = ""
            }
        },
        new[]
        {
            new CodeGeneratorConfiguration
            {
                DtoProjectAdditionalPath = " "
            }
        }
    };

    public static IEnumerable<object[]> AddDtoAdditionalPathToTypeInfo_MultipleTypeInfosData =
        new List<object[]>
        {
            new[]
            {
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "bla"
                    }
                },
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "bla",
                        OriginalDtoProjectAdditionalPathToken = "path"
                    }
                },
            },
            new[]
            {
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "bla"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "bla2"
                    },
                },
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "bla",
                        OriginalDtoProjectAdditionalPathToken = "path"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "bla2",
                        OriginalDtoProjectAdditionalPathToken = "path"
                    }
                },
            }
        };

    [Theory]
    [MemberData(nameof(AddDtoAdditionalPathToTypeInfoData))]
    public void AddDtoAdditionalPathToTypeInfo(CodeGeneratorConfiguration configuration)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>();
        List<TypeInfo> expected = new List<TypeInfo>();

        // Act
        _sut.GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo(typeInfos, configuration);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void AddDtoAdditionalPathToTypeInfo_WhenConfigIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo(null!,
                new CodeGeneratorConfiguration());
        };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void AddDtoAdditionalPathToTypeInfo_TypeInfoIsEmpty()
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>();
        List<TypeInfo> expected = new List<TypeInfo>();

        // Act
        _sut.GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo(typeInfos,
            new CodeGeneratorConfiguration { DtoProjectAdditionalPath = "something" });

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(AddDtoAdditionalPathToTypeInfo_MultipleTypeInfosData))]
    public void AddDtoAdditionalPathToTypeInfo_MultipleTypeInfos(List<TypeInfo> orig, List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            DtoProjectAdditionalPath = "path"
        };

        // Act
        _sut.GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo(orig, configuration);

        // Assert
        orig.Should().BeEquivalentTo(expected);
    }
}