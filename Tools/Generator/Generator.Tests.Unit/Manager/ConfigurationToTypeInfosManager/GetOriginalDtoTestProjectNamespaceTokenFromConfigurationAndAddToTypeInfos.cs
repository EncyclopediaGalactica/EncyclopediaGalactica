namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos_Data =
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
                        OriginalTypeNameToken = "asd"
                    }
                },
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd",
                        OriginalDtoTestProjectNamespaceToken = "namespace"
                    }
                },
            },
            new[]
            {
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd2"
                    },
                },
                new List<TypeInfo>
                {
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd",
                        OriginalDtoTestProjectNamespaceToken = "namespace"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd2",
                        OriginalDtoTestProjectNamespaceToken = "namespace"
                    },
                },
            },
        };

    [Fact]
    public void GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos_GeneratorConfigIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
                new List<TypeInfo>(),
                null);
        };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos_TargetDirectoryNull(
        string? dtoProjectNamespaceValue)
    {
        // Arrange
        List<TypeInfo> list = new List<TypeInfo>
        {
            new TypeInfo
            {
                OriginalTypeNameToken = "asd"
            }
        };
        List<TypeInfo> expected = new List<TypeInfo>
        {
            {
                new TypeInfo
                {
                    OriginalTypeNameToken = "asd"
                }
            }
        };

        // Act
        Action action = () =>
        {
            _sut.GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
                list,
                new CodeGeneratorConfiguration
                {
                    DtoTestProjectNameSpace = dtoProjectNamespaceValue!
                });
        };

        // Assert
        action.Should().NotThrow();
        list.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos_TypeInfoEmpty()
    {
        // Arrange
        List<TypeInfo> list = new List<TypeInfo>();

        // Act
        Action action = () =>
        {
            _sut.GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
                list,
                new CodeGeneratorConfiguration
                {
                    SolutionBasePath = "asd"
                });
        };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos_Data))]
    public void GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> input,
        List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            DtoTestProjectNameSpace = "namespace"
        };

        // Act
        _sut.GetOriginalDtoTestProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
            input,
            configuration);

        // Assert
        input.Should().BeEquivalentTo(expected);
    }
}