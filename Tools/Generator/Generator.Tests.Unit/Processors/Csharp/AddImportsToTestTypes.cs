namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    public static IEnumerable<object[]> AddImportsToTestTypesData = new List<object[]>
    {
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "typeName",
                    Namespace = "some.name.pace",
                }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "typeName"
                }
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "typeName",
                }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "typeName"
                }
            }
        },
        new[]
        {
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "typeName",
                    Namespace = "some.name.space",
                    TypeName = "TypeName"
                }
            },
            new List<TypeInfo>
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "typeName2"
                }
            }
        },
    };

    [Theory]
    [MemberData(nameof(AddImportsToTestTypesData))]
    public void AddImportsToTestTypes(List<TypeInfo> dtoTypeInfos, List<TypeInfo> dtoTestTypeInfos)
    {
        // Arrange && Act
        _sut.AddImportsToTestTypes(dtoTestTypeInfos, dtoTypeInfos);

        // Assert
        if (string.IsNullOrEmpty(dtoTypeInfos[0].Namespace))
        {
            dtoTestTypeInfos[0].Imports.Count.Should().Be(0);
        }
        else if (dtoTypeInfos[0].OriginalTypeNameToken != dtoTestTypeInfos[0].OriginalTypeNameToken)
        {
            dtoTestTypeInfos[0].Imports.Count.Should().Be(0);
        }
        else
        {
            dtoTestTypeInfos[0].Imports.Count.Should().Be(1);
            dtoTestTypeInfos[0].Imports[0].Should().Be("some.name.pace");
        }
    }
}