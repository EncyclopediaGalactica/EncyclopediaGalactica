namespace ValidatorService;

using EncyclopediaGalactica.SourceFormats.Worker.Entities;
using FluentValidation;

public class SourceFormatNodeValidator : AbstractValidator<SourceFormatNode>
{
    public const string Add = "Add";
    public const string Update = "Update";

    public SourceFormatNodeValidator()
    {
        RuleSet(Add, () =>
        {
            RuleFor(p => p.Id).Equal(0);
            RuleFor(p => p.Name).NotEmpty().NotNull().NotEqual(" ");
            RuleFor(p => p.Name!.Length).GreaterThanOrEqualTo(3);
        });

        RuleSet(Update, () =>
        {
            RuleFor(p => p.Id).NotEqual(0);
            RuleFor(p => p.Name).NotEmpty().NotNull().NotEqual(" ");
            RuleFor(p => p.Name!.Length).GreaterThanOrEqualTo(3);
        });
    }
}