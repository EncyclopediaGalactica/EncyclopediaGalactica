namespace EncyclopediaGalactica.Scenarios.Storage.Edge;

using Common;
using EncyclopediaGalactica.Storage.Entities;
using FluentValidation;
using FluentValidation.Results;

public record AddEdgeScenarioInput
{
    public long Id { get; set; }
    public long FromVertexId { get; set; }
    public long ToVertexId { get; set; }
    public long EdgeTypeId { get; set; }
}

public record AddEdgeScenarioResult : AddEdgeScenarioInput;

public static class EdgeExtensions
{
    public static Either<EgError, AddEdgeScenarioResult> ToAddEdgeScenarioResult(this EdgeEntity input)
    {
        try
        {
            AddEdgeScenarioResult result = new()
            {
                Id = input.Id,
                FromVertexId = input.FromVertexId,
                ToVertexId = input.ToVertexId,
                EdgeTypeId = input.EdgeTypeId,
            };
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }

    public static Either<EgError, EdgeEntity> ToEdgeEntity(this AddEdgeScenarioInput input)
    {
        try
        {
            EdgeEntity result = new()
            {
                Id = input.Id,
                FromVertexId = input.FromVertexId,
                ToVertexId = input.ToVertexId,
                EdgeTypeId = input.EdgeTypeId,
            };
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message));
        }
    }
}

public class AddEdgeScenarioInputValidator : AbstractValidator<AddEdgeScenarioInput>
{
    public AddEdgeScenarioInputValidator()
    {
        RuleFor(r => r.Id).Equal(0);
        RuleFor(r => r.FromVertexId).GreaterThanOrEqualTo(1);
        RuleFor(r => r.ToVertexId).GreaterThanOrEqualTo(1);
        RuleFor(r => r.EdgeTypeId).GreaterThanOrEqualTo(1);
    }

    public Either<EgError, AddEdgeScenarioInput> IsValid(AddEdgeScenarioInput input)
    {
        ValidationResult? validationResult = Validate(input);
        return validationResult.IsValid == true ? Right(input) : Left(validationResult.ToEgError());
    }
}