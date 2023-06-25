namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Configuration;

using System.Text.RegularExpressions;
using FluentValidation;

public class CodeGeneratorConfigurationValidator : AbstractValidator<CodeGeneratorConfiguration>
{
    public CodeGeneratorConfigurationValidator()
    {
        RuleFor(p => p.OpenApiSpecificationPath)
            .NotNull()
            .NotEmpty()
            .WithMessage("OpenApi specification path must be defined.");

        RuleFor(p => p.Lang)
            .NotNull()
            .NotEmpty()
            .WithMessage("Lang must be defined");

        RuleFor(p => p.TargetDirectory)
            .NotNull()
            .NotEmpty()
            .WithMessage("Target directory must be specified");

        RuleFor(p => p.SolutionBaseNamespace)
            .NotNull()
            .NotEmpty()
            .WithMessage("Base namespace must be defined.");
        When(p => !string.IsNullOrEmpty(p.SolutionBaseNamespace) && !string.IsNullOrWhiteSpace(p.SolutionBaseNamespace),
            () =>
            {
#pragma warning disable CS8604
                RuleFor(p => char.IsLetter(p.SolutionBaseNamespace.First()))
                    .Equal(true)
                    .WithMessage("Solution base namespace must start with letter");
#pragma warning restore CS8604
            });

        RuleFor(p => p.SolutionName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Solution name must be provided");
        When(p => !string.IsNullOrEmpty(p.SolutionName) && !string.IsNullOrWhiteSpace(p.SolutionName), () =>
        {
#pragma warning disable CS8604
            RuleFor(p => Regex.IsMatch(p.SolutionName, "^[a-zA-Z0-9.]*$"))
                .Equal(true)
                .WithMessage("Solution name must contain only alphanumeric and . (dot)");
            RuleFor(p => char.IsLetter(p.SolutionName.First()))
                .Equal(true)
                .WithMessage("Solution name must start with letter");
#pragma warning restore CS8604
        });
    }
}