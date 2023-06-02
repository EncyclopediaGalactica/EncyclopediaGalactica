namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator.Managers.FileManager;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class FileManager_Should
{
    private readonly IFileManager _sut = new FileManagerImpl();

    [Theory]
    [InlineData(null, false)]
    [InlineData("Should_DeleteFile.txt", true)]
    [InlineData("Should_DeleteFile.txt", false)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    public void Should_DeleteFile(string path, bool exist)
    {
        // Arrange
        if (exist)
        {
            _sut.CheckIfExistsOrCreate(path);
        }

        // Act
        _sut.DeleteFile(path);

        // Assert
        _sut.CheckIfFileExist(path).Should().BeFalse();
    }

    [Theory]
    [InlineData("Should_CheckIfExistsOrCreate.txt", true, true)]
    [InlineData("Should_CheckIfExistsOrCreate3.txt", false, true)]
    public void Should_CheckIfExistsOrCreate_ValidInput(string s, bool exist, bool expected)
    {
        // Arrange
        if (exist)
        {
            _sut.CheckIfExistsOrCreate(s);
        }

        // Act
        _sut.CheckIfExistsOrCreate(s);
        bool result = _sut.CheckIfFileExist(s);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_CheckIfExistsOrCreate_InvalidInput(string s)
    {
        // Act
        Action action = () => { _sut.CheckIfExistsOrCreate(s); };

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData("Should_CheckIfFileExist.txt", true, true)]
    [InlineData("Should_CheckIfFileExist2.txt", false, false)]
    [InlineData(null, false, false)]
    [InlineData("", false, false)]
    [InlineData(" ", false, false)]
    public void Should_CheckIfFileExist(string path, bool exist, bool expected)
    {
        // Arrange
        if (exist)
        {
            _sut.CheckIfExistsOrCreate(path);
        }

        // Act
        bool result = _sut.CheckIfFileExist(path);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Should_ReadAllText_ThereIsContentInTheFile()
    {
        // Arrange
        string content = "some content";
        string path = "Should_ReadAllText_ThereIsContentInTheFile.txt";
        _sut.WriteContentIntoFile(content, path);

        // Act
        string result = _sut.ReadAllText(path);

        // Assert
        result.Should().Be(content);
        _sut.DeleteFile(path);
    }
}