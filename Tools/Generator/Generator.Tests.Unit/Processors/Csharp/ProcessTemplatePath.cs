namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("/one/two", "/one/two")]
    [InlineData("one/two", "one/two")]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData(" ", null)]
    public void ProcessDtoTemplatePath(string templatePath, string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo()
        };

        // Act
        _sut.ProcessTemplatePath(fileInfos, templatePath);

        // Assert
        if (!string.IsNullOrEmpty(templatePath)
            && !string.IsNullOrWhiteSpace(templatePath)
            && templatePath[0].ToString() != "/")
        {
            expected = $"{Directory.GetCurrentDirectory()}/{templatePath}";
        }

        fileInfos[0].TemplateAbsolutePathWithFileName.Should().Be(expected);
    }
}