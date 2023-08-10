namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Configuration;

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

        RuleFor(p => p.SolutionName)
            .NotNull()
            .WithMessage("Solution name must be provided")
            .DependentRules(() =>
            {
                RuleFor(p => p.SolutionName.Trim())
                    .NotEmpty()
                    .WithMessage("Solution name must be provided")
                    .DependentRules(() =>
                    {
                        RuleFor(p => char.IsNumber(p.SolutionName.First()))
                            .NotEqual(true)
                            .WithMessage("Solution name cannot start with a number")
                            .DependentRules(() =>
                            {
                                RuleFor(p => "!@#$%^&*()_-=+,/\\|'\";:?><".Any(p.SolutionName.Trim().Contains))
                                    .NotEqual(true)
                                    .WithMessage("Only dot (.) is the allowed special character in the solution name");
                            });
                    });
            });

        RuleFor(p => p.TargetDirectory)
            .NotNull()
            .NotEmpty()
            .WithMessage("Target directory path must be specified.");

        RuleFor(p => p.SolutionBaseNamespace)
            .NotNull()
            .WithMessage("Solution base namespace must not be null.")
            .DependentRules(() =>
            {
                RuleFor(p => p.SolutionBaseNamespace.Trim())
                    .NotEmpty()
                    .WithMessage("Solution base namespace must be defined.");

                RuleFor(p => p.SolutionBaseNamespace.First())
                    .Must(char.IsLetter)
                    .WithMessage("Base namespace must start with a letter.");
            });

        RuleFor(p => p.DtoProjectName).NotNull().NotEmpty()
            .WithMessage("Dto project name must be provided");

        RuleFor(p => p.DtoProjectBasePath).NotNull().NotEmpty()
            .WithMessage("Dto project base path must be provided");

        RuleFor(p => p.DtoProjectTestUnitName).NotNull().NotEmpty()
            .WithMessage("Dto test project name must be provided");

        RuleFor(p => p.DtoProjectTestUnitBasePath).NotNull().NotEmpty()
            .WithMessage("Dto test project base path is required");

        When(p => string.IsNullOrEmpty(p.DtoProjectBasePath) == false
                  && string.IsNullOrWhiteSpace(p.DtoProjectBasePath) == false,
            () =>
            {
                RuleFor(p => p.DtoProjectBasePath[0].ToString() != "/").Equal(true)
                    .WithMessage("Dto project base path url must not be absolute path. It has to be relative path.");
            });

        When(p => string.IsNullOrEmpty(p.DtoProjectAdditionalPath) == false
                  && string.IsNullOrWhiteSpace(p.DtoProjectAdditionalPath) == false,
            () =>
            {
                RuleFor(p => p.DtoProjectAdditionalPath[0].ToString() != "/").Equal(true)
                    .WithMessage("Dto project additional path must not be absolute path. It must be relative path.");
            });
    }
}