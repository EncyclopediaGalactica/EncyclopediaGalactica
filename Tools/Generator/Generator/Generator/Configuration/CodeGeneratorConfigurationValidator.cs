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

        RuleFor(p => p.SolutionFileType)
            .NotNull()
            .WithMessage("Solution file type must be provided")
            .DependentRules(() =>
            {
                RuleFor(pp => pp.SolutionFileType.Trim())
                    .NotEmpty()
                    .WithMessage("Solution file type must be provided")
                    .DependentRules(() =>
                    {
                        RuleFor(ppp => ppp.SolutionFileType.All(char.IsLetter))
                            .NotEqual(false)
                            .WithMessage("Solution file type can contain only letters.");
                    });
            });

        RuleFor(p => p.SolutionProjectFileType)
            .NotNull()
            .WithMessage("Solution project file type must be provided")
            .DependentRules(() =>
            {
                RuleFor(pp => pp.SolutionProjectFileType.Trim())
                    .NotEmpty()
                    .WithMessage("Solution project file type must be provided")
                    .DependentRules(() =>
                    {
                        RuleFor(ppp => ppp.SolutionProjectFileType.All(char.IsLetter))
                            .NotEqual(false)
                            .WithMessage("Solution project file type can contain only letters.");
                    });
            });

        RuleFor(p => p.DtoProjectName)
            .NotNull()
            .WithMessage("Dto project name must be provided")
            .DependentRules(() =>
            {
                RuleFor(pp => pp.DtoProjectName.Trim())
                    .NotEmpty()
                    .WithMessage("Dto project name must be provided")
                    .DependentRules(() =>
                    {
                        RuleFor(ppp => char.IsLetter(ppp.DtoProjectName.First()))
                            .Equal(true)
                            .WithMessage("Dto project name must have only letters.");

                        RuleFor(ppp => "!@#$%^&*()=+,/\\|'\";:?><".Any(ppp.DtoProjectName.Contains))
                            .Equal(false)
                            .WithMessage("Dto project name cannot contain special characters other than " +
                                         "dot '.', dash '-' and underscore '_'");

                        When(ppp => ppp.DtoProjectName.Contains("."), () =>
                        {
                            RuleFor(pppp => char.IsLetter(pppp.DtoProjectName[pppp.DtoProjectName.IndexOf(".") + 1]))
                                .NotEqual(false)
                                .WithMessage("When Dto project name contains a dot ('.') the following " +
                                             "character must be a letter.");
                        });
                    });
            });

        RuleFor(p => p.DtoProjectNameSpace)
            .NotNull()
            .WithMessage("Dto Namespace value must be provided")
            .DependentRules(() =>
            {
                RuleFor(p => p.DtoProjectNameSpace)
                    .NotEmpty()
                    .WithMessage("Dto Project Namespace must not be empty")
                    .DependentRules(() =>
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        RuleFor(p => p.DtoProjectNameSpace.Trim().Length)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                            .GreaterThanOrEqualTo(1)
                            .WithMessage("Dto Project Name must not consists only of space characters.")
                            .DependentRules(() =>
                            {
#pragma warning disable CS8604 // Possible null reference argument.
                                RuleFor(p => p.DtoProjectNameSpace.All(char.IsLetter))
#pragma warning restore CS8604 // Possible null reference argument.
                                    .Equal(true)
                                    .WithMessage("Dto Project Namespace must contains only letters");
                            });
                    });
            });

        RuleFor(p => p.DtoProjectPath)
            .NotNull()
            .NotEmpty()
            .WithMessage("Dto project base path must be provided")
            .DependentRules(() =>
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                RuleFor(p => p.DtoProjectPath[0].ToString() != "/").Equal(true)
                    .WithMessage("Dto project base path url must not be absolute path. It has to be relative path.");
#pragma warning disable CS8604 // Possible null reference argument.
                RuleFor(p => p.DtoProjectPath.All(char.IsLetterOrDigit)).Equal(true)
#pragma warning restore CS8604 // Possible null reference argument.
                    .WithMessage("Dto Project Path can contain only alphanumeric characters.");
                RuleFor(p => p.DtoProjectPath.StartsWith("./")).NotEqual(true)
                    .WithMessage("Dto Project Path cannot start with ./ value.");
                RuleFor(p => p.DtoProjectPath.Trim().Length).GreaterThanOrEqualTo(1)
                    .WithMessage("Dto Project Path cannot be only whitespaces!");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            });
    }
}