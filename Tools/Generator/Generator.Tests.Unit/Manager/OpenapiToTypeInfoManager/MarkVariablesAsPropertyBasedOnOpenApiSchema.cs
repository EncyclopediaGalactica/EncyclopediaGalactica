namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenapiToTypeInfoManager;

using FluentAssertions;
using Generator.Models;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class OpenApiToTypeInfoManager_Should
{
    public static IEnumerable<object[]> MarkVariablesAsPropertyBasedOnOpenApiSchemaData = new List<object[]>
    {
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "firstSchema",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "tag"
                        }
                    }
                }
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "firstSchema",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "tag"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "secondSchema",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "tag"
                        }
                    }
                },
            },
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "firstSchema",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "tag"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "secondSchema",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "id"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "name"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "tag"
                        }
                    }
                },
                new TypeInfo
                {
                    OriginalTypeNameToken = "thirdSchema",
                    VariableInfos = new List<VariableInfo>
                    {
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "code"
                        },
                        new VariableInfo
                        {
                            OriginalVariableNameToken = "message"
                        }
                    }
                }
            }
        }
    };

    [Theory]
    [MemberData(nameof(MarkVariablesAsPropertyBasedOnOpenApiSchemaData))]
    public void MarkVariablesAsPropertyBasedOnOpenApiSchema(List<TypeInfo> typeInfos)
    {
        // Arrange
        OpenApiDocument openApiSchema = SchemasTestData.GetTestDataForMarkVariablesAsPropertyBasedOnOpenApiSchema();

        // Act
        _sut.MarkVariablesAsPropertyBasedOnOpenApiSchema(typeInfos, openApiSchema);

        // Assert
        if (typeInfos.Count == 1)
        {
            typeInfos.Find(p => p.OriginalTypeNameToken == "firstSchema")!
                .VariableInfos.Where(p => p.IsProperty).ToList().Count
                .Should().Be(3);
        }

        if (typeInfos.Count == 2)
        {
            typeInfos.Find(p => p.OriginalTypeNameToken == "firstSchema")!
                .VariableInfos.Where(p => p.IsProperty).ToList().Count
                .Should().Be(3);

            typeInfos.Find(p => p.OriginalTypeNameToken == "secondSchema")!
                .VariableInfos.Where(p => p.IsProperty).ToList().Count
                .Should().Be(3);
        }

        if (typeInfos.Count == 3)
        {
            typeInfos.Find(p => p.OriginalTypeNameToken == "firstSchema")!
                .VariableInfos.Where(p => p.IsProperty).ToList().Count
                .Should().Be(3);

            typeInfos.Find(p => p.OriginalTypeNameToken == "secondSchema")!
                .VariableInfos.Where(p => p.IsProperty).ToList().Count
                .Should().Be(3);

            typeInfos.Find(p => p.OriginalTypeNameToken == "thirdSchema")!
                .VariableInfos.Where(p => p.IsProperty).ToList().Count
                .Should().Be(2);
        }
    }
}