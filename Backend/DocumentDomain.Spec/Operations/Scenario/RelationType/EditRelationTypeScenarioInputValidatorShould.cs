namespace DocumentDomain.Spec.Operations.Scenario.RelationType;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;
using FluentAssertions;
using FluentValidation.Results;

public class EditRelationTypeScenarioInputValidatorShould
{
    private readonly AddRelationTypeScenarioInputValidator validator = new();

    [Theory]
    [ClassData(typeof(AddRelationTypeScenarioInputValidationInvalidInputData))]
    public void ShowInvalidState_WhenInputIsInvalid(RelationTypeInput input)
    {
        ValidationResult result = validator.Validate(input);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public void ShowValidState_WhenInputIsValid()
    {
        ValidationResult validationResult = validator.Validate(
            new RelationTypeInput
            {
                Id = 0,
                Name = "asd",
                Description = "asd",
            }
        );
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }
}

[ExcludeFromCodeCoverage]
public class EditRelationTypeScenarioInputValidationInvalidInputData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 0,
                Name = "asd",
                Description = "asd",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = null,
                Description = "asd",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = string.Empty,
                Description = "asd",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "   ",
                Description = "asd",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "as",
                Description = "asd",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = " as ",
                Description = "asd",
            },
        };

        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "asd",
                Description = null,
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "asd",
                Description = string.Empty,
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "asd",
                Description = "   ",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "asd",
                Description = "as",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "asd",
                Description = "  as  ",
            },
        };

        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "asd",
                Description = "asd",
            },
        };
        yield return new object[]
        {
            new RelationTypeInput
            {
                Id = 1,
                Name = "asd",
                Description = "asd",
            },
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}