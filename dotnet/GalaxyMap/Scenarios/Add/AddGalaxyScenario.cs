namespace EncyclopediaGalactica.Modules.GalaxyMap.Scenarios.Add;

using Common;
using FluentValidation;
using FluentValidation.Results;

public class AddGalaxyScenario
{
    public Either<EgError, AddGalaxyScenarioResult> Execute(
        AddGalaxyScenarioInput input
    )
    {
        Either < EgError,
    }
}

public record AddGalaxyScenarioInput(
    string Name,
    string Description);

public record AddGalaxyScenarioResult();

public class AddGalaxyScenarioInputValidator : AbstractValidator<AddGalaxyScenarioInput>
{
    public AddGalaxyScenarioInputValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        When(
            r => !string.IsNullOrEmpty(r.Name),
            () =>
            {
                RuleFor(r => r.Name.Trim().Length).GreaterThanOrEqualTo(3);
            }
        );
        When(
            r => !string.IsNullOrEmpty(r.Description),
            () =>
            {
                RuleFor(r => r.Description.Trim().Length).GreaterThanOrEqualTo(3);
            }
        );
    }

    public Either<EgError, AddGalaxyScenarioInput> IsValid(AddGalaxyScenarioInput input)
    {
        ValidationResult? validationResult = Validate(input);
        if (validationResult != null && validationResult.IsValid == false)
        {
            return Left(validationResult.ToEgError());
        }

        return Right(input);
    }
}