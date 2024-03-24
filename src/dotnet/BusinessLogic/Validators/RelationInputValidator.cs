namespace EncyclopediaGalactica.BusinessLogic.Validators;

using Contracts;
using FluentValidation;

public class RelationInputValidator : AbstractValidator<RelationInput>
{
    public RelationInputValidator()
    {
        When(p => p is null, () =>
        {
            string m = $"Input for {nameof(RelationInput)} validation is null.";
            throw new ValidationException(m);
        });

        When(p => p is not null, () =>
        {
            RuleSet(Operations.Add, () =>
            {
                RuleFor(p => p.Id).Equal(0)
                    .WithMessage($"{nameof(RelationInput)}.{nameof(RelationInput.Id)} must be zero");

                RuleFor(p => p.LeftEndId).GreaterThan(0)
                    .WithMessage(
                        $"{nameof(RelationInput)}.{nameof(RelationInput.LeftEndId)} must be greater than zero.");

                RuleFor(p => p.RightEndId).GreaterThan(0)
                    .WithMessage(
                        $"{nameof(RelationInput)}.{nameof(RelationInput.RightEndId)} must be greater than zero.");
            });
        });
    }
}