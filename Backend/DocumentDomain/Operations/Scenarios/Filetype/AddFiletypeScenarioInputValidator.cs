namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation;

public class AddFiletypeScenarioInputValidator : AbstractValidator<FiletypeInput>
{
    public AddFiletypeScenarioInputValidator()
    {
        RuleFor(r => r.Id)
            .Equal(0)
            .WithMessage($"The id of {nameof(Filetype)} entity must be zero during creation.");

        RuleFor(r => r.Name)
            .NotNull()
            .WithMessage($"The name of {nameof(Filetype)} entity must not be null.");

        When(w => w.Name is not null, () =>
        {
            RuleFor(w => w.Name)
                .NotEmpty()
                .WithMessage($"The name of {nameof(Filetype)} entity must not be empty");
            RuleFor(w => w.Name.Trim().Length)
                .GreaterThanOrEqualTo(3)
                .WithMessage($"The name length of {nameof(Filetype)} must be equal or greater to 3 chars.");
        });

        RuleFor(r => r.Description)
            .NotNull()
            .WithMessage($"The description of {nameof(Filetype)} entity must not be null.");

        When(w => w.Description is not null, () =>
        {
            RuleFor(w => w.Description)
                .NotEmpty()
                .WithMessage($"The description of {nameof(Filetype)} entity must not be empty");
            RuleFor(w => w.Description.Trim().Length)
                .GreaterThanOrEqualTo(3)
                .WithMessage($"The description length of {nameof(Filetype)} must be equal or greater to 3 chars.");
        });

        RuleFor(r => r.FileExtension)
            .NotNull()
            .WithMessage($"The filetype extension of {nameof(Filetype)} entity must not be null.");

        When(w => w.FileExtension is not null, () =>
        {
            RuleFor(w => w.FileExtension.Trim())
                .NotEmpty()
                .WithMessage($"The filetype extension of {nameof(Filetype)} entity must not be empty");
        });
    }
}