namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]>
        GetOriginalDtoTestProjectBasePathAndAddToTypeInfos_WhenPathValueIsNullEmptyOrWhitespaceData =
            new List<object[]>
            {
                new[] { new CodeGeneratorConfiguration { DtoProjectTestUnitBasePath = null } },
                new[] { new CodeGeneratorConfiguration { DtoProjectTestUnitBasePath = "" } },
                new[] { new CodeGeneratorConfiguration { DtoProjectTestUnitBasePath = " " } },
            };

    public static IEnumerable<object[]> GetOriginalDtoTestProjectBasePathAndAddToTypeInfos_MultipleTypeInfoData =
        new List<object[]>
        {
            new[]
            {
                new List<TypeInfo>(),
                new List<TypeInfo>()
            },
            new[]
            {
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "first"
                    }
                },
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "first",
                        OriginalDtoTestProjectBasePathToken = "path"
                    }
                }
            },
            new[]
            {
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "first"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "second"
                    }
                },
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "first",
                        OriginalDtoTestProjectBasePathToken = "path"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "second",
                        OriginalDtoTestProjectBasePathToken = "path"
                    }
                }
            },
        };

    [Theory]
    [MemberData(nameof(GetOriginalDtoTestProjectBasePathAndAddToTypeInfos_WhenPathValueIsNullEmptyOrWhitespaceData))]
    public void GetOriginalDtoTestProjectBasePathAndAddToTypeInfos_WhenPathValueIsNullEmptyOrWhitespace(
        CodeGeneratorConfiguration configuration)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo { OriginalTypeNameToken = "bla" }
        };
        List<TypeInfo> expected = new List<TypeInfo>
        {
            new TypeInfo { OriginalTypeNameToken = "bla" }
        };

        // Act

        _sut.GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos(typeInfos, configuration);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetOriginalDtoTestProjectBasePathAndAddToTypeInfos_WhenConfigurationIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos(null!,
                new CodeGeneratorConfiguration());
        };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(GetOriginalDtoTestProjectBasePathAndAddToTypeInfos_MultipleTypeInfoData))]
    public void GetOriginalDtoTestProjectBasePathAndAddToTypeInfos_MultipleTypeInfo(
        List<TypeInfo> input,
        List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            DtoProjectTestUnitBasePath = "path"
        };

        // Act
        _sut.GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos(input, configuration);

        // Assert
        input.Should().BeEquivalentTo(expected);
    }
}