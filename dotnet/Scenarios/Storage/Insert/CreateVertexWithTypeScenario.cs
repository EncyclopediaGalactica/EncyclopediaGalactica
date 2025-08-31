namespace EncyclopediaGalactica.Scenarios.Storage.Insert;

using Common;
using EdgeType;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Entities;
using EncyclopediaGalactica.Storage.Repository.Edge;
using EncyclopediaGalactica.Storage.Repository.EdgeType;
using EncyclopediaGalactica.Storage.Repository.Vertex;
using FluentValidation;
using FluentValidation.Results;

public class CreateVertexWithTypeScenario(
    EdgeRepository edgeRepository,
    EdgeTypeRepository edgeTypeRepository,
    AddEdgeTypeScenario addEdgeTypeScenario,
    VertexRepository vertexRepository,
    CreateVertexWithTypeScenarioInputValidator validator,
    StorageContext ctx
)
{
    public Either<EgError, CreateVertexWithTypeScenarioResult> Execute(CreateVertexWithTypeScenarioInput input) =>
        from validatedInput in validator.IsValid(input)
        from edgeTypeEntity in FindOrCreateEdgeType(validatedInput)
        from mappedVertexEntity in input.ToVertexEntity()
        from recordedVertex in vertexRepository.AddWithEdgeType(mappedVertexEntity, edgeTypeEntity, ctx)
        from mappedResult in recordedVertex.ToCreateVertexWithTypeScenarioResult()
        select mappedResult;

    private Either<EgError, EdgeTypeEntity> FindOrCreateEdgeType(
        CreateVertexWithTypeScenarioInput input
    ) =>
        from edgeTypeOptional in edgeTypeRepository.FindByName(input.VertexType, ctx)
        from edgeTypeEntity in edgeTypeOptional.Match(
            yolo => yolo,
            () => from addEdgeTypeScenarioInput in input.ToAddEdgeTypeScenarioInput()
                from addEdgeTypeResult in addEdgeTypeScenario.Execute(addEdgeTypeScenarioInput)
                from addedEdgeType in addEdgeTypeResult.ToEdgeTypeEntity()
                select addedEdgeType
        )
        select edgeTypeEntity;
}

public record CreateVertexWithTypeScenarioResult(
    long VertexId,
    string VertexType);

public record CreateVertexWithTypeScenarioInput(
    string VertexType,
    Dictionary<string, object> Data);

public static class CreateVertexWithTypeScenarioExtensions
{
    public static Either<EgError, CreateVertexWithTypeScenarioResult> ToCreateVertexWithTypeScenarioResult(
        this VertexEntity vertexEntity
    )
    {
        try
        {
            if (vertexEntity.EdgeTypes.Count == 0)
            {
                return Left(
                    new EgError($"Vertex {vertexEntity.Id} does not have edge type connecting to it defining type.")
                );
            }

            CreateVertexWithTypeScenarioResult result = new(vertexEntity.Id, vertexEntity.EdgeTypes.First().Name);
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }

    public static Either<EgError, VertexEntity> ToVertexEntity(
        this CreateVertexWithTypeScenarioInput input
    )
    {
        try
        {
            return Right(
                new VertexEntity() { Data = input.Data, }
            );
        }
        catch (Exception e)
        {
            return Left(new EgError(e.Message, e.StackTrace));
        }
    }

    public static Either<EgError, AddEdgeTypeScenarioInput> ToAddEdgeTypeScenarioInput(
        this CreateVertexWithTypeScenarioInput input
    )
    {
        try
        {
            return Right(
                new AddEdgeTypeScenarioInput()
                {
                    Name = input.VertexType,
                    Description = $"Edge type has been added by {nameof(CreateVertexWithTypeScenario)}.",
                }
            );
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }
}

public class CreateVertexWithTypeScenarioInputValidator : AbstractValidator<CreateVertexWithTypeScenarioInput>
{
    public CreateVertexWithTypeScenarioInputValidator()
    {
        RuleFor(x => x.VertexType).NotNull().NotEmpty();
        When(
            x => !string.IsNullOrEmpty(x.VertexType),
            () => RuleFor(x => x.VertexType.Trim().Length).GreaterThanOrEqualTo(3)
        );
    }

    public Either<EgError, CreateVertexWithTypeScenarioInput> IsValid(CreateVertexWithTypeScenarioInput input)
    {
        ValidationResult? validationResult = Validate(input);
        if (validationResult != null && !validationResult.IsValid)
        {
            return Left(validationResult.ToEgError());
        }

        return Right(input);
    }
}