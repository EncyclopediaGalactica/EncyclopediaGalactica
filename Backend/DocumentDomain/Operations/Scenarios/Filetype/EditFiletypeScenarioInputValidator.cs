namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class EditFiletypeScenarioInputValidator : AbstractValidator<FiletypeInput>
{
    public EditFiletypeScenarioInputValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"The id of {nameof(FiletypeInput)} entity must be greater or equals to 1.");

        RuleFor(r => r.Name)
            .NotNull()
            .WithMessage($"The name of {nameof(FiletypeInput)} entity must not be null.");

        When(w => w.Name is not null, () =>
        {
            RuleFor(w => w.Name)
                .NotEmpty()
                .WithMessage($"The name of {nameof(FiletypeInput)} entity must not be empty");
            RuleFor(w => w.Name.Trim().Length)
                .GreaterThanOrEqualTo(3)
                .WithMessage($"The name length of {nameof(FiletypeInput)} must be equal or greater to 3 chars.");
        });

        RuleFor(r => r.Description)
            .NotNull()
            .WithMessage($"The description of {nameof(FiletypeInput)} entity must not be null.");

        When(w => w.Description is not null, () =>
        {
            RuleFor(w => w.Description)
                .NotEmpty()
                .WithMessage($"The description of {nameof(FiletypeInput)} entity must not be empty");
            RuleFor(w => w.Description.Trim().Length)
                .GreaterThanOrEqualTo(3)
                .WithMessage($"The description length of {nameof(FiletypeInput)} must be equal or greater to 3 chars.");
        });

        RuleFor(r => r.FileExtension)
            .NotNull()
            .WithMessage($"The filetype extension of {nameof(FiletypeInput)} entity must not be null.");

        When(w => w.FileExtension is not null, () =>
        {
            RuleFor(w => w.FileExtension.Trim())
                .NotEmpty()
                .WithMessage($"The filetype extension of {nameof(FiletypeInput)} entity must not be empty");
        });
    }
}