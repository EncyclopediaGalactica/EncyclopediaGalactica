namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]>
        GetOriginalDtoProjectBasePathAndAddToTypeInfos_WhenPathValueIsNullEmptyOrWhitespaceData =
            new List<object[]>
            {
                new[] { new CodeGeneratorConfiguration { DtoProjectBasePath = null } },
                new[] { new CodeGeneratorConfiguration { DtoProjectBasePath = "" } },
                new[] { new CodeGeneratorConfiguration { DtoProjectBasePath = " " } },
            };

    public static IEnumerable<object[]> GetOriginalDtoProjectBasePathAndAddToTypeInfos_MultipleTypeInfoData =
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
                        OriginalDtoProjectBasePathToken = "path"
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
                        OriginalDtoProjectBasePathToken = "path"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "second",
                        OriginalDtoProjectBasePathToken = "path"
                    }
                }
            },
        };

    [Theory]
    [MemberData(nameof(GetOriginalDtoProjectBasePathAndAddToTypeInfos_WhenPathValueIsNullEmptyOrWhitespaceData))]
    public void GetOriginalDtoProjectBasePathAndAddToTypeInfos_WhenPathValueIsNullEmptyOrWhitespace(
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
        _sut.GetOriginalDtoProjectBasePathFromConfigurationAndAddToTypeInfos(typeInfos, configuration);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetOriginalDtoProjectBasePathAndAddToTypeInfos_WhenConfigurationIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalDtoProjectBasePathFromConfigurationAndAddToTypeInfos(null!,
                new CodeGeneratorConfiguration());
        };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(GetOriginalDtoProjectBasePathAndAddToTypeInfos_MultipleTypeInfoData))]
    public void GetOriginalDtoProjectBasePathAndAddToTypeInfos_MultipleTypeInfo(
        List<TypeInfo> input,
        List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            DtoProjectBasePath = "path"
        };

        // Act
        _sut.GetOriginalDtoProjectBasePathFromConfigurationAndAddToTypeInfos(input, configuration);

        // Assert
        input.Should().BeEquivalentTo(expected);
    }
}