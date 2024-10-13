namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using FluentValidation.Results;

public class AddFiletypeScenarioInputValidatorShould
{
    private AddFiletypeScenarioInputValidator validator = new();

    [Theory]
    [ClassData(typeof(AddFiletypeScenarioInputValidationInvalidInputData))]
    public void ShowInvalidState_WhenInputIsInvalid(FiletypeInput input)
    {
        ValidationResult result = validator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public void ShowValidState_WhenInputIsValid()
    {
        ValidationResult validationResult = validator.Validate(
            new FiletypeInput { Id = 0, Name = "asd", Description = "asd", FileExtension = "asd" });
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }
}

[ExcludeFromCodeCoverage]
public class AddFiletypeScenarioInputValidationInvalidInputData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = null, Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = string.Empty, Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = "   ", Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = "as", Description = "asd", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = " as ", Description = "asd", FileExtension = "asd" } };

        yield return new object[] { new FiletypeInput { Id = 1, Name = "asd", Description = null, FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = "asd", Description = string.Empty, FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = "asd", Description = "   ", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = "asd", Description = "as", FileExtension = "asd" } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = "asd", Description = "  as  ", FileExtension = "asd" } };

        yield return new object[] { new FiletypeInput { Id = 0, Name = "asd", Description = "asd", FileExtension = null } };
        yield return new object[] { new FiletypeInput { Id = 0, Name = "asd", Description = "asd", FileExtension = string.Empty } };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}