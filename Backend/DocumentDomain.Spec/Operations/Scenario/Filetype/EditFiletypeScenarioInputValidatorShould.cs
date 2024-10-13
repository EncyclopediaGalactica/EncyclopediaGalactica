namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Entity;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using FluentValidation.Results;

public class EditFiletypeScenarioInputValidatorShould
{
    private EditFiletypeScenarioInputValidator _validator = new();


    [Theory]
    [ClassData(typeof(EditFiletypeScenarioInputValidatorInvalidInputData))]
    public void ShowsInvalidResult_WhenInputIsInvalid(FiletypeInput filetypeInput)
    {
        ValidationResult result = _validator.Validate(filetypeInput);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public void ShowsValidResult_WhenInputIsValid()
    {
        ValidationResult result = _validator.Validate(new FiletypeInput
        {
            Id = 1,
            Name = "asd",
            Description = "asd",
            FileExtension = "asd"
        });
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}

[ExcludeFromCodeCoverage]
public class EditFiletypeScenarioInputValidatorInvalidInputData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new FiletypeInput { Id = 0, Name = "asd", Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = null, Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = string.Empty, Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "   ", Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = " as  ", Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = null, FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = string.Empty, FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = "   ", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = " as  ", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = "asd", FileExtension = null } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = "asd", FileExtension = string.Empty } };
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = "asd", FileExtension = "   " } };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}