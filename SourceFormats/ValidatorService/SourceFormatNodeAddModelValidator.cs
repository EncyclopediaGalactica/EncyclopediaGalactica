namespace EncyclopediaGalactica.SourceFormats.ValidatorService;

using FluentValidation;
using Sdk.Models;

public class SourceFormatNodeAddModelValidator : AbstractValidator<SourceFormatNodeAddRequestModel>
{
    public const string Add = "Add";

    public SourceFormatNodeAddModelValidator()
    {
#pragma warning disable CS8602
        RuleSet(Add, () =>
        {
            RuleFor(p => p.Id).Equal(0);
            RuleFor(p => p.Name).NotNull();
            When(p => p.Name is not null, () =>
            {
                RuleFor(p => p.Name.Trim())
                    .NotNull()
                    .NotEmpty();
                RuleFor(p => p.Name.Length)
                    .GreaterThanOrEqualTo(3);
            });
        });
#pragma warning restore CS8602
    }
}