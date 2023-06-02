namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfosData =
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
                        OriginalBaseNamespaceToken = "namespace"
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
                        OriginalBaseNamespaceToken = "namespace"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd2",
                        OriginalBaseNamespaceToken = "namespace"
                    },
                },
            },
        };

    [Fact]
    public void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos_GeneratorConfigIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
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
    public void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos_TargetDirectoryNull(
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
            _sut.GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
                list,
                new CodeGeneratorConfiguration
                {
                    DtoProjectNameSpace = dtoProjectNamespaceValue!
                });
        };

        // Assert
        action.Should().NotThrow();
        list.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos_TypeInfoEmpty()
    {
        // Arrange
        List<TypeInfo> list = new List<TypeInfo>();

        // Act
        Action action = () =>
        {
            _sut.GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
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
    [MemberData(nameof(GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfosData))]
    public void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> input,
        List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            SolutionBaseNamespace = "namespace"
        };

        // Act
        _sut.GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
            input,
            configuration);

        // Assert
        input.Should().BeEquivalentTo(expected);
    }
}