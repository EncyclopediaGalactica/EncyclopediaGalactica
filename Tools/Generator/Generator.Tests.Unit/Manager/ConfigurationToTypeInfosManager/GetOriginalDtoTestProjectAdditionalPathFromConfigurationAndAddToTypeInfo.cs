namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> AddDtoTestProjectAdditionalPathToTypeInfoData = new List<object[]>
    {
        new[]
        {
            new CodeGeneratorConfiguration()
        },
        new[]
        {
            new CodeGeneratorConfiguration
            {
                DtoTestProjectAdditionalPath = ""
            }
        },
        new[]
        {
            new CodeGeneratorConfiguration
            {
                DtoTestProjectAdditionalPath = " "
            }
        }
    };

    public static IEnumerable<object[]> AddDtoTestProjectAdditionalPathToTypeInfo_MultipleTypeInfosData =
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
                        OriginalDtoTestProjectAdditionalPathToken = "path"
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
                        OriginalDtoTestProjectAdditionalPathToken = "path"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "bla2",
                        OriginalDtoTestProjectAdditionalPathToken = "path"
                    }
                },
            }
        };

    [Theory]
    [MemberData(nameof(AddDtoTestProjectAdditionalPathToTypeInfoData))]
    public void GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo(
        CodeGeneratorConfiguration configuration)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>();
        List<TypeInfo> expected = new List<TypeInfo>();

        // Act
        _sut.GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo(typeInfos, configuration);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo_WhenConfigIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo(null!,
                new CodeGeneratorConfiguration());
        };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo_TypeInfoIsEmpty()
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>();
        List<TypeInfo> expected = new List<TypeInfo>();

        // Act
        _sut.GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo(typeInfos,
            new CodeGeneratorConfiguration { DtoProjectAdditionalPath = "something" });

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(AddDtoTestProjectAdditionalPathToTypeInfo_MultipleTypeInfosData))]
    public void GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo_MultipleTypeInfos(
        List<TypeInfo> orig, List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            DtoTestProjectAdditionalPath = "path"
        };

        // Act
        _sut.GetOriginalDtoTestProjectAdditionalPathFromConfigurationAndAddToTypeInfo(orig, configuration);

        // Assert
        orig.Should().BeEquivalentTo(expected);
    }
}