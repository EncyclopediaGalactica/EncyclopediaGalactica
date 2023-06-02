namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator.Managers.PathManager;
using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class PathManager_Should
{
    private readonly IPathManager _sut = new PathManagerImpl();

    [Theory]
    [InlineData("foo", "bar", "foo/bar")]
    [InlineData(" foo", "bar", "foo/bar")]
    [InlineData("foo ", "bar", "foo/bar")]
    [InlineData("foo", " bar", "foo/bar")]
    [InlineData("foo", "bar ", "foo/bar")]
    [InlineData(null, "bar", "bar")]
    [InlineData("", "bar", "bar")]
    [InlineData(" ", "bar", "bar")]
    [InlineData("foo", null, "foo")]
    [InlineData("foo", "", "foo")]
    [InlineData("foo", " ", "foo")]
    public void BuildPathString_2Params(string s1, string s2, string expected)
    {
        // Arrange && Act
        string result = _sut.BuildPathString(s1, s2);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("foo", "bar", "bla", "foo/bar/bla")]
    [InlineData(" foo", "bar", "bla", "foo/bar/bla")]
    [InlineData("foo ", "bar", "bla", "foo/bar/bla")]
    [InlineData("foo", " bar", "bla", "foo/bar/bla")]
    [InlineData("foo", "bar ", "bla", "foo/bar/bla")]
    [InlineData("foo", "bar", " bla", "foo/bar/bla")]
    [InlineData("foo", "bar", "bla ", "foo/bar/bla")]
    [InlineData(null, "bar", "bla", "bar/bla")]
    [InlineData("", "bar", "bla", "bar/bla")]
    [InlineData(" ", "bar", "bla", "bar/bla")]
    [InlineData("foo", null, "bla", "foo/bla")]
    [InlineData("foo", "", "bla", "foo/bla")]
    [InlineData("foo", " ", "bla", "foo/bla")]
    [InlineData("foo", "bar", null, "foo/bar")]
    [InlineData("foo", "bar", "", "foo/bar")]
    [InlineData("foo", "bar", " ", "foo/bar")]
    [InlineData(null, null, "bla", "bla")]
    [InlineData("", null, "bla", "bla")]
    [InlineData(" ", null, "bla", "bla")]
    [InlineData(null, "", "bla", "bla")]
    [InlineData(null, " ", "bla", "bla")]
    [InlineData("foo", null, null, "foo")]
    [InlineData("foo", "", null, "foo")]
    [InlineData("foo", " ", null, "foo")]
    [InlineData("foo", null, "", "foo")]
    [InlineData("foo", null, " ", "foo")]
    [InlineData(null, "bar", null, "bar")]
    [InlineData("", "bar", null, "bar")]
    [InlineData(" ", "bar", null, "bar")]
    [InlineData(null, "bar", "", "bar")]
    [InlineData(null, "bar", " ", "bar")]
    public void BuildPathString_3Params(
        string s1,
        string s2,
        string s3,
        string expected)
    {
        // Arrange && Act
        string result = _sut.BuildPathString(s1, s2, s3);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("asd", "file.txt", "asd/file.txt")]
    [InlineData("/asd", "file.txt", "/asd/file.txt")]
    [InlineData("asd/", "file.txt", "asd/file.txt")]
    public void BuildPathStringWithFilename(string path, string filename, string expected)
    {
        // Arrange && Act
        string result = _sut.BuildPathStringWithFilename(path, filename);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "file.txt")]
    [InlineData("", "file.txt")]
    [InlineData(" ", "file.txt")]
    [InlineData("asd", null)]
    [InlineData("asd", "")]
    [InlineData("asd", " ")]
    public void BuildPathStringWithFilename_Throw(
        string path,
        string filename)
    {
        // Arrange && Act
        Action action = () => { _sut.BuildPathStringWithFilename(path, filename); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("CheckOrCreatePath", false, "CheckOrCreatePath")]
    [InlineData("CheckOrCreatePath2", true, "CheckOrCreatePath2")]
    [InlineData("CheckOrCreatePath3/3", false, "CheckOrCreatePath3")]
    [InlineData("CheckOrCreatePath4/5/6", false, "CheckOrCreatePath4")]
    [InlineData("CheckOrCreatePath5/6", true, "CheckOrCreatePath5")]
    public void CheckOrCreatePath(string path, bool exist, string deletePath)
    {
        // Arrange
        if (exist)
        {
            _sut.CreatePath(path);
        }

        // Act
        _sut.CheckOrCreatePath(path);
        bool result = _sut.IsPathExists(path);

        // Assert
        result.Should().BeTrue();
        _sut.DeletePath(deletePath, true);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CheckOrCreatePath_Throw(string path)
    {
        // Arrange && Act
        Action action = () => { _sut.CheckOrCreatePath(path); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("IsPathExists", true, true, "IsPathExists")]
    [InlineData("IsPathExists2", false, false, null)]
    public void IsPathExists(string path, bool exists, bool expected, string deletePath)
    {
        // Arrange
        if (exists)
        {
            _sut.CheckOrCreatePath(path);
        }

        // Act
        bool result = _sut.IsPathExists(path);

        // Assert
        result.Should().Be(expected);
        if (!string.IsNullOrEmpty(deletePath))
        {
            _sut.DeletePath(deletePath);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void IsPathExists_Throw(string path)
    {
        // Arrange && Act
        Action action = () => { _sut.IsPathExists(path); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreatePath_Throw(string path)
    {
        // Arrange && Act
        Action action = () => { _sut.CreatePath(path); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void DeletePath_Throw(string path)
    {
        // Arrange && Act
        Action action = () => { _sut.DeletePath(path); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("asd", "asd")]
    [InlineData("asd/", "asd")]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public void CheckAndRemoveEndSlash(string path, string expected)
    {
        // Arrange && Act
        string result = _sut.CheckAndRemoveEndSlash(path);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("asd", false)]
    [InlineData("asd", true)]
    [InlineData("/asd", true)]
    public void CheckIfPathAbsoluteOrMakeItOne(string s, bool abs)
    {
        // Arrange
        if (!abs)
        {
            s = _sut.BuildPathString(_sut.GetCurrentDirectory(), s);
        }

        // Act
        string result = _sut.CheckIfPathAbsoluteOrMakeItOne(s);

        // Assert
        result[0].ToString().Should().Be("/");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CheckIfPathAbsoluteOrMakeItOne_Throw(string s)
    {
        // Arrange && Act
        Action action = () => { _sut.CheckIfPathAbsoluteOrMakeItOne(s); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }
}