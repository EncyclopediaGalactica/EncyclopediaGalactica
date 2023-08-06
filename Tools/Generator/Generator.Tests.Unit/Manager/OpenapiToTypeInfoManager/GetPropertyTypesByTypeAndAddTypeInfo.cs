namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenapiToTypeInfoManager;

using FluentAssertions;
using Generator.Models;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class OpenApiToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetPropertyTypesByTypeAndAddTypeInfoData = new List<object[]>
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
                new TypeInfo { OriginalTypeNameToken = "oneProperty" }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                }
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                }
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        }
                    }
                }
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
                new TypeInfo { OriginalTypeNameToken = "twoPropertiesWithAllRequired" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoPropertiesWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                    }
                }
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
                new TypeInfo { OriginalTypeNameToken = "twoPropertiesWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "threeProperties" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoPropertiesWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "threeProperties",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "desc",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                    }
                }
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo { OriginalTypeNameToken = "oneProperty" },
                new TypeInfo { OriginalTypeNameToken = "onePropertyWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "twoProperties" },
                new TypeInfo { OriginalTypeNameToken = "twoPropertiesWithAllRequired" },
                new TypeInfo { OriginalTypeNameToken = "threeProperties" },
                new TypeInfo { OriginalTypeNameToken = "threePropertiesWithAllRequired" },
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "oneProperty",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "onePropertyWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoProperties",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "twoPropertiesWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "threeProperties",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "desc",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "threePropertiesWithAllRequired",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id",
                            OriginalVariableTypeNameToken = "integer",
                            OriginalVariableTypeFormat = "int64"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "desc",
                            OriginalVariableTypeNameToken = "string",
                            OriginalVariableTypeFormat = ""
                        },
                    }
                }
            },
        },
    };

    [Theory]
    [MemberData(nameof(GetPropertyTypesByTypeAndAddTypeInfoData))]
    public void GetPropertyTypesByTypeAndAddTypeInfo(List<TypeInfo> typeInfos, List<TypeInfo> expected)
    {
        // Arrange
        (OpenApiDocument openApi,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> schemaWithPropertyNamesAndTypes)
            testData = SchemasTestData.GetPropertyTypesBySchemaTestData();
        _sut.GetPropertyNamesByTypeAndAddToTypeInfo(typeInfos, testData.openApi);

        // Act
        _sut.GetPropertyTypesByTypeAndAddTypeInfo(typeInfos, testData.openApi);

        // Assert
        typeInfos.Should().BeEquivalentTo(expected);
    }
}