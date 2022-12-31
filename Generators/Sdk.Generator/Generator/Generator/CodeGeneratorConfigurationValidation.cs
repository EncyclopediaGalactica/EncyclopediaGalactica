namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;

using FluentValidation;

public class CodeGeneratorConfigurationValidation : AbstractValidator<CodeGeneratorConfiguration>
{
    public CodeGeneratorConfigurationValidation()
    {
        RuleFor(p => p.OpenApiSpecificationPath).NotNull().NotEmpty();
        RuleFor(p => p.SolutionDirectory).NotNull().NotEmpty();
        RuleFor(p => p.SolutionBaseNamespace).NotNull().NotEmpty();
    }
}