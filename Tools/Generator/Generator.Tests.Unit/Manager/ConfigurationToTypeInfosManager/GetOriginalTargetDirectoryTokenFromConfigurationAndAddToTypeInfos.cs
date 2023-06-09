namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetOriginalTargetDirectoryTokenFromConfiguration_AndAddToTypeInfosData =
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
                        OriginalTargetDirectoryToken = "targetpath"
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
                        OriginalTargetDirectoryToken = "targetpath"
                    },
                    new TypeInfo
                    {
                        OriginalTypeNameToken = "asd2",
                        OriginalTargetDirectoryToken = "targetpath"
                    },
                },
            },
        };

    [Fact]
    public void GetOriginalTargetDirectoryTokenFromConfiguration_AndAddToTypeInfos_ConfigurationIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalTargetDirectoryTokenFromConfiguration(
                new List<TypeInfo>(),
                null!);
        };

        // Assert
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GetOriginalTargetDirectoryTokenFromConfiguration_AndAddToTypeInfos_TargetDirectoryNull(
        string? targetDirectoryValue)
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
            _sut.GetOriginalTargetDirectoryTokenFromConfiguration(
                list,
                new CodeGeneratorConfiguration
                {
                    TargetDirectory = targetDirectoryValue!
                });
        };

        // Assert
        action.Should().NotThrow();
        list.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetOriginalTargetDirectoryTokenFromConfiguration_AndAddToTypeInfos_TypeInfoEmpty()
    {
        // Arrange
        List<TypeInfo> list = new List<TypeInfo>();

        // Act
        Action action = () =>
        {
            _sut.GetOriginalTargetDirectoryTokenFromConfiguration(
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
    [MemberData(nameof(GetOriginalTargetDirectoryTokenFromConfiguration_AndAddToTypeInfosData))]
    public void GetOriginalTargetDirectoryTokenFromConfiguration_AndAddToTypeInfos(
        List<TypeInfo> input,
        List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            TargetDirectory = "targetpath"
        };

        // Act
        _sut.GetOriginalTargetDirectoryTokenFromConfiguration(
            input,
            configuration);

        // Assert
        input.Should().BeEquivalentTo(expected);
    }
}