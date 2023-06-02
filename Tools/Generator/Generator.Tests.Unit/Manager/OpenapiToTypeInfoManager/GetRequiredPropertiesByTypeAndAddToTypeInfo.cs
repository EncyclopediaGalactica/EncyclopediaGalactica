namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenapiToTypeInfoManager;

using FluentAssertions;
using Generator.Models;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class OpenApiToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetRequiredPropertiesByTypeAndAddToTypeInfoData = new List<object[]>
    {
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo()
            },
            new List<TypeInfo>
            {
                new TypeInfo()
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "noneRequired"
                }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "noneRequired"
                }
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "noneRequired"
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneRequired"
                }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "noneRequired"
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneRequired",
                    RequiredProperties = new List<string>
                    {
                        "name"
                    }
                }
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "noneRequired"
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneRequired"
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoRequired"
                }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "noneRequired"
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneRequired",
                    RequiredProperties = new List<string>
                    {
                        "name"
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoRequired",
                    RequiredProperties = new List<string>
                    {
                        "id",
                        "name"
                    }
                }
            }
        },
    };

    [Theory]
    [MemberData(nameof(GetRequiredPropertiesByTypeAndAddToTypeInfoData))]
    public void GetRequiredPropertiesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, List<TypeInfo> expected)
    {
        // Arrange
        (OpenApiDocument openApi, Dictionary<string, List<string>> requiredPropertiesBySchemaName) testData =
            SchemasTestData.GetRequiredPropertiesBySchemaTestData();

        // Act
        _sut.GetRequiredPropertiesByTypeAndAddToTypeInfo(typeInfos, testData.openApi);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }
}