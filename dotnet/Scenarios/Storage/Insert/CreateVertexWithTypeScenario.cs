namespace EncyclopediaGalactica.Scenarios.Storage.Insert;

using Common;
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
    VertexRepository vertexRepository,
    CreateVertexWithTypeScenarioInputValidator validator,
    StorageContext ctx
)
{
    public Either<EgError, CreateVertexWithTypeScenarioResult> Execute(CreateVertexWithTypeScenarioInput input)
    {
        Either<EgError, Option<EdgeTypeEntity>> edgeTypeEntityOptional =
            from validatedInput in validator.IsValid(input)
            from edgeTypeResult in edgeTypeRepository.FindByName(validatedInput.VertexType, ctx)
            select edgeTypeResult;
        edgeTypeEntityOptional.IfRight(brightSide =>
            {
                brightSide.IfNone(() =>
                    {
                        // create the missing EdgeTypeEntity
                    }
                );
            }
        );
        if (edgeTypeEntityOptional.IsLeft)
        {
            EgError egError = new(string.Empty);
            edgeTypeEntityOptional.IfLeft(error => { egError = error; });
            return Left(egError);
        }
    }
}

public record CreateVertexWithTypeScenarioResult(
    long VertexId,
    string VertexType);

public record CreateVertexWithTypeScenarioInput(
    string VertexType,
    Dictionary<string, object> Data);

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