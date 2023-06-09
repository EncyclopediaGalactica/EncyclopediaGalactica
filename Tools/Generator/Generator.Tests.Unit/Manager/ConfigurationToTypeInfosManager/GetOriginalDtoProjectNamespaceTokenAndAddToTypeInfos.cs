namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetOriginalDtoProjectNamespaceTokenAndAddToTypeInfosData =
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
                        OriginalDtoNamespaceToken = "namespace"
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
                        OriginalDtoNamespaceToken = "namespace"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd2",
                        OriginalDtoNamespaceToken = "namespace"
                    },
                },
            },
        };

    [Fact]
    public void GetOriginalDtoProjectNamespaceTokenAndAddToTypeInfos_GeneratorConfigIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
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
    public void GetOriginalDtoProjectNamespaceTokenAndAddToTypeInfos_TargetDirectoryNull(
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
            _sut.GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
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
    public void GetOriginalDtoProjectNamespaceTokenAndAddToTypeInfos_TypeInfoEmpty()
    {
        // Arrange
        List<TypeInfo> list = new List<TypeInfo>();

        // Act
        Action action = () =>
        {
            _sut.GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
                list,
                new CodeGeneratorConfiguration
                {
                    TargetDirectory = "asd"
                });
        };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(GetOriginalDtoProjectNamespaceTokenAndAddToTypeInfosData))]
    public void GetOriginalDtoProjectNamespaceTokenAndAddToTypeInfos(
        List<TypeInfo> input,
        List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            DtoProjectNameSpace = "namespace"
        };

        // Act
        _sut.GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
            input,
            configuration);

        // Assert
        input.Should().BeEquivalentTo(expected);
    }
}