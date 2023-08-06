namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.ConfigurationToTypeInfosManager;

using FluentAssertions;
using Generator.Configuration;
using Generator.Models;
using Xunit;

public partial class ConfigurationToTypeInfoManager_Should
{
    public static IEnumerable<object[]> GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfosData =
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
    public void GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos_ConfigurationIsNull()
    {
        // Arrange && Act
        Action action = () =>
        {
            _sut.GetOriginalTargetPathBaseFromConfiguration(
                new List<TypeInfo>(),
                null!);
        };

        //
        action.Should().NotThrow();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos_TargetDirectoryNull(
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
            _sut.GetOriginalTargetPathBaseFromConfiguration(
                list,
                new CodeGeneratorConfiguration
                {
                    SolutionBasePath = targetDirectoryValue!
                });
        };

        // Assert
        action.Should().NotThrow();
        list.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos_TypeInfoEmpty()
    {
        // Arrange
        List<TypeInfo> list = new List<TypeInfo>();

        // Act
        Action action = () =>
        {
            _sut.GetOriginalTargetPathBaseFromConfiguration(
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
    [MemberData(nameof(GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfosData))]
    public void GetOriginalTargetPathBaseFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> input,
        List<TypeInfo> expected)
    {
        // Arrange
        CodeGeneratorConfiguration configuration = new CodeGeneratorConfiguration
        {
            SolutionBasePath = "targetpath"
        };

        // Act
        _sut.GetOriginalTargetPathBaseFromConfiguration(
            input,
            configuration);

        // Assert
        input.Should().BeEquivalentTo(expected);
    }
}