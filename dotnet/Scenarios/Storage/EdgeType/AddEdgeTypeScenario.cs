namespace EncyclopediaGalactica.Scenarios.Storage.EdgeType;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Entities;
using EncyclopediaGalactica.Storage.Repository.EdgeType;
using FluentValidation;
using FluentValidation.Results;

public class AddEdgeTypeScenario(
    StorageContext ctx,
    EdgeTypeRepository repository,
    AddEdgeTypeScenarioInputValidator validator
)
{
    public Either<EgError, AddEdgeTypeScenarioResult> Execute(AddEdgeTypeScenarioInput input) =>
        from validatedInput in validator.IsValid(input)
        from mappedInput in validatedInput.ToEntity()
        from recordedEntity in repository.Add(mappedInput, ctx)
        from toResult in recordedEntity.ToEdgeTypeResult()
        select toResult;
}

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

public record AddEdgeTypeScenarioResult(
    long Id,
    string Name,
    string Description);

public static class EdgeTypeExtensions
{
    public static Either<EgError, List<AddEdgeTypeScenarioResult>> ToEdgeTypeResults(this List<EdgeTypeEntity> entities)
    {
        try
        {
            return Right(
                entities.Select(item => new AddEdgeTypeScenarioResult(item.Id, item.Name, item.Description)).ToList()
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }

    public static Either<EgError, EdgeTypeEntity> ToEntity(this AddEdgeTypeScenarioInput input)
    {
        try
        {
            return Right(
                new EdgeTypeEntity() { Name = input.Name, Description = input.Description, }
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }

    public static Either<EgError, EdgeTypeEntity> ToEdgeTypeEntity(this AddEdgeTypeScenarioResult result)
    {
        try
        {
            return Right(
                new EdgeTypeEntity() { Id = result.Id, Name = result.Name, Description = result.Description, }
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }

    public static Either<EgError, AddEdgeTypeScenarioResult> ToEdgeTypeResult(this EdgeTypeEntity edgeType)
    {
        try
        {
            return Right(new AddEdgeTypeScenarioResult(edgeType.Id, edgeType.Name, edgeType.Description));
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}