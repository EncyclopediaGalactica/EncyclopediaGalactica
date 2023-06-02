namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator.Managers.StringManager;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class StringManager_Should
{
    private IStringManager _sut = new Generator.Managers.StringManager.StringManagerImpl();

    [Theory]
    [InlineData("some", "thing", "something")]
    [InlineData(" some", "thing", "something")]
    [InlineData("some ", "thing", "something")]
    [InlineData("some", " thing", "something")]
    [InlineData("some", "thing ", "something")]
    [InlineData(" some ", " thing ", "something")]
    [InlineData(null, "thing", "thing")]
    [InlineData("", "thing", "thing")]
    [InlineData(" ", "thing", "thing")]
    [InlineData("some", null, "some")]
    [InlineData("some", "", "some")]
    [InlineData("some", " ", "some")]
    public void TwoParameters(string s1, string s2, string expected)
    {
        // Arrange && Act
        string result = _sut.Concat(s1, s2);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("some", "thing", "foo", "somethingfoo")]
    [InlineData(null, "thing", "foo", "thingfoo")]
    [InlineData("", "thing", "foo", "thingfoo")]
    [InlineData("some", null, "foo", "somefoo")]
    [InlineData("some", "", "foo", "somefoo")]
    [InlineData("some", " ", "foo", "somefoo")]
    [InlineData("some", "thing", null, "something")]
    [InlineData("some", "thing", "", "something")]
    [InlineData("some", "thing", " ", "something")]
    [InlineData("some", "thing", "foo", "somethingfoo")]
    [InlineData(" some", "thing", "foo", "somethingfoo")]
    [InlineData("some ", "thing", "foo", "somethingfoo")]
    [InlineData("some", " thing", "foo", "somethingfoo")]
    [InlineData("some", "thing ", "foo", "somethingfoo")]
    [InlineData("some", "thing", " foo", "somethingfoo")]
    [InlineData("some", "thing", "foo ", "somethingfoo")]
    [InlineData(" some ", " thing ", " foo ", "somethingfoo")]
    public void ThreeParameters(string s1, string s2, string s3, string expected)
    {
        // Arrange && Act
        string result = _sut.Concat(s1, s2, s3);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("lower", "Lower")]
    [InlineData("Lower", "Lower")]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public void MakeFirstCharUpperCase(string s1, string expected)
    {
        // Arrange && Act
        string result = _sut.MakeFirstCharUpperCase(s1);

        // Assert
        result.Should().Be(result);
    }

    [Theory]
    [InlineData("name", "space", "name.space")]
    [InlineData(null, "space", "space")]
    [InlineData("", "space", "space")]
    [InlineData(" ", "space", "space")]
    [InlineData("name", null, "name")]
    [InlineData("name", "", "name")]
    [InlineData("name", " ", "name")]
    [InlineData("name", "Space", "name.Space")]
    [InlineData(".name", "space", "name.space")]
    [InlineData("name.", "space", "name.space")]
    [InlineData("name", ".space", "name.space")]
    [InlineData("name", "space.", "name.space")]
    [InlineData(".name.", ".space.", "name.space")]
    [InlineData(".na.me.", ".space.", "na.me.space")]
    [InlineData("name", ".spa.ce.", "name.space")]
    [InlineData(null, null, "")]
    [InlineData("", null, "")]
    [InlineData(" ", null, "")]
    [InlineData(null, "", "")]
    [InlineData(null, " ", "")]
    public void ConcatenateCSharpNamespaceTokens(string? s1, string? s2, string expected)
    {
        // Arrange && Act
        string result = _sut.ConcatCsharpNamespaceTokens(s1, s2);

        // Assert
        result.Should().Be(result);
    }

    [Theory]
    [InlineData("namespace", "Namespace")]
    [InlineData("name.sp.ace", "Name.Sp.Ace")]
    [InlineData("n.a.m.e.s.p.a.c.e", "N.A.M.E.S.P.A.C.E")]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData(" ", "")]
    public void MakeUppercaseTheCharAfterTheDot(string? s, string expected)
    {
        // Arrange && Act
        string? result = _sut.MakeUppercaseTheCharAfterTheDot(s);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("lower", "lower")]
    [InlineData("Lower", "lower")]
    [InlineData("LOWER", "lower")]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData(" ", "")]
    public void MakeTheStringLowerCase(string? s, string expected)
    {
        // Arrange && Assert
        string result = _sut.ToLowerCase(s);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("asd", false)]
    [InlineData("/asd", true)]
    [InlineData("a/asd", false)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    public void CheckIfFirstCharIsSlashAndThrow(string s, bool shouldThrow)
    {
        // Arrange && Act
        Action action = () => { _sut.CheckIfFirstCharIsSlashAndThrow(s); };

        // Assert
        if (shouldThrow)
        {
            action.Should().Throw<ArgumentException>();
        }
        else
        {
            action.Should().NotThrow();
        }
    }

    [Theory]
    [InlineData("asd", "asd")]
    [InlineData("asd/", "asd")]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData(" ", "")]
    public void CheckIfLastCharSlashAndRemoveIt(string? s, string expected)
    {
        // Arrange && Act
        string result = _sut.CheckIfLastCharSlashAndRemoveIt(s);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("asd", false)]
    [InlineData("/asd", true)]
    [InlineData("/", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    public void IsFirstCharIsASlash(string s, bool expected)
    {
        // Arrange && Assert
        bool result = _sut.IsFirstCharIsASlash(s);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("asd", "Asd")]
    [InlineData("a_sd", "ASd")]
    [InlineData("_asd_", "Asd")]
    [InlineData("asd_asd", "AsdAsd")]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public void MakeSnakeCaseToPascalCase(string? s, string expected)
    {
        // Arrange && Act
        string? result = _sut.MakeSnakeCaseToPascalCase(s);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("asd", ".asd")]
    [InlineData(".asd", ".asd")]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public void CheckIfFirstCharIsDotOrAddIt(string? s, string? expected)
    {
        // Arrange

        // Act
        string? result = _sut.CheckIfFirstCharIsDotOrAddIt(s);

        // Assert
        result.Should().Be(expected);
    }
}