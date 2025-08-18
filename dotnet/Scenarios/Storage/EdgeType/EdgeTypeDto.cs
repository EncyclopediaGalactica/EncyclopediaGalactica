namespace EncyclopediaGalactica.Scenarios.Storage.EdgeType;

using Common;
using EncyclopediaGalactica.Storage.Entities;
using FluentValidation;
using FluentValidation.Results;

public record EdgeTypeResult(
    long Id,
    string Name,
    string Description);

public record AddEdgeTypeScenarioInput
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

public class AddEdgeTypeScenarioInputValidator : AbstractValidator<AddEdgeTypeScenarioInput>
{
    public AddEdgeTypeScenarioInputValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name.Trim().Length).GreaterThanOrEqualTo(3);
        RuleFor(x => x.Description).NotEmpty();
    }

    public Either<EgError, AddEdgeTypeScenarioInput> IsValid(AddEdgeTypeScenarioInput input)
    {
        ValidationResult? validationResult = Validate(input);
        return validationResult.IsValid == true ? Right(input) : Left(validationResult.ToEgError());
    }
}

public static class EdgeTypeExtensions
{
    public static Either<EgError, EdgeTypeEntity> ToEntity(this AddEdgeTypeScenarioInput input)
    {
        try
        {
            return Right(new EdgeTypeEntity() { Name = input.Name, Description = input.Description, });
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }

    public static Either<EgError, EdgeTypeResult> ToEdgeTypeResult(this EdgeTypeEntity edgeType)
    {
        try
        {
            return Right(new EdgeTypeResult(edgeType.Id, edgeType.Name, edgeType.Description));
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}