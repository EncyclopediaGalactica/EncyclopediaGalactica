namespace EncyclopediaGalactica.Scenarios.Storage.Edge;

using Common;
using EncyclopediaGalactica.Storage.Entities;
using FluentValidation;
using FluentValidation.Results;

public record AddEdgeScenarioInput(
    long FromVertexId,
    long ToVertexId,
    long EdgeTypeId
);

public record AddEdgeScenarioResult(
    long Id,
    long FromVertexId,
    long ToVertexId,
    long EdgeTypeId
);

public static class EdgeExtensions
{
    public static Either<EgError, AddEdgeScenarioResult> ToAddEdgeScenarioResult(this EdgeEntity input)
    {
        try
        {
            return Right(
                new AddEdgeScenarioResult(
                    input.Id,
                    input.FromVertexId,
                    input.ToVertexId,
                    input.EdgeTypeId
                )
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }

    public static Either<EgError, EdgeEntity> ToEdgeEntity(this AddEdgeScenarioInput input)
    {
        try
        {
            EdgeEntity result = new()
            {
                FromVertexId = input.FromVertexId, ToVertexId = input.ToVertexId, EdgeTypeId = input.EdgeTypeId,
            };
            return Right(result);
        }
        catch (Exception e)
        {
            string message = $"{nameof(EdgeEntity)}.{nameof(ToEdgeEntity)}: {e.Message}";
            return Left(new EgError(message, e.StackTrace));
        }
    }
}

public class AddEdgeScenarioInputValidator : AbstractValidator<AddEdgeScenarioInput>
{
    public AddEdgeScenarioInputValidator()
    {
        RuleFor(r => r.FromVertexId).GreaterThanOrEqualTo(0);
        RuleFor(r => r.ToVertexId).GreaterThanOrEqualTo(0);
        RuleFor(r => r.EdgeTypeId).GreaterThanOrEqualTo(1);
    }

    public Either<EgError, AddEdgeScenarioInput> IsValid(AddEdgeScenarioInput input)
    {
        try
        {
            ValidationResult? validationResult = Validate(input);
            return validationResult.IsValid == true ? Right(input) : Left(validationResult.ToEgError());
        }
        catch (Exception e)
        {
            string message = $"{nameof(AddEdgeScenarioInput)}.{nameof(IsValid)}: {e.Message}";
            return Left(new EgError(message, e.StackTrace));
        }
    }
}