namespace EncyclopediaGalactica.BusinessLogic.Validators;

public class StructureNodeValidator : AbstractValidator<StructureNode>
{
    public StructureNodeValidator()
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