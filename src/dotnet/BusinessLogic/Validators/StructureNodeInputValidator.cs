namespace EncyclopediaGalactica.BusinessLogic.Validators;

using Contracts;
using FluentValidation;

public class StructureNodeInputValidator : AbstractValidator<StructureNodeInput>
{
    public StructureNodeInputValidator()
    {
        RuleSet(Operations.Add, () =>
        {
            RuleFor(p => p.Id)
                .Equal(0)
                .WithMessage("Id must be zero.");
            RuleFor(p => p.DocumentId)
                .GreaterThan(0)
                .WithMessage("Document Id must not be zero");
        });
    }
}