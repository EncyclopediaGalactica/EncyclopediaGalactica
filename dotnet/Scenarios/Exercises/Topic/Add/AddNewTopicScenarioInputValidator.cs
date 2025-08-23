namespace EncyclopediaGalactica.Scenarios.Exercises.Topic.Add;

using Common;
using FluentValidation;
using FluentValidation.Results;
using Logic.Repository.Models;
using static Prelude;

public class AddNewTopicScenarioInputValidator : AbstractValidator<TopicEntity>
{
    public AddNewTopicScenarioInputValidator()
    {
        RuleFor(r => r.Id).Equal(0);
        When(
            w => !string.IsNullOrEmpty(w.Name) && !string.IsNullOrWhiteSpace(w.Name),
            () =>
            {
                RuleFor(r => r.Name.Trim().Length).GreaterThanOrEqualTo(3);
            }
        );
        When(
            w => !string.IsNullOrEmpty(w.Reference) && !string.IsNullOrWhiteSpace(w.Reference),
            () =>
            {
                RuleFor(r => r.Reference.Trim().Length).GreaterThanOrEqualTo(3);
            }
        );
    }

    public Either<EgError, TopicEntity> IsValid(
        TopicEntity input
    )
    {
        ValidationResult? result = Validate(input);
        if (!result.IsValid)
        {
            return Left(result.ToEgError());
        }

        return Right(input);
    }
}