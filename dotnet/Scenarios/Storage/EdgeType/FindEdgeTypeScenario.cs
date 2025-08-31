namespace EncyclopediaGalactica.Scenarios.Storage.EdgeType;

using Common;
using EncyclopediaGalactica.Storage.Entities;
using EncyclopediaGalactica.Storage.Repository.EdgeType;

public class FindEdgeTypeScenario(
    EdgeTypeRepository repository
)
{
    public Either<EgError, FindEdgeTypeScenarioResult> Execute(FindEdgeTypeScenarioInput input) =>
        from entity in repository.FindByName(input.EdgeType)
        from mappedResult in entity.ToFindEdgeTypeScenarioResult()
        select mappedResult;
}

public static class FindEdgeTypeScenarioExtensions
{
    public static Either<EgError, FindEdgeTypeScenarioResult> ToFindEdgeTypeScenarioResult(this EdgeTypeEntity input)
    {
        try
        {
            return Right(
                new FindEdgeTypeScenarioResult(input.Id)
            );
        }
        catch (Exception ex)
        {
            return Left(new EgError(ex.Message, ex.StackTrace));
        }
    }
}

public record FindEdgeTypeScenarioInput(
    string EdgeType);

public record FindEdgeTypeScenarioResult(
    long EdgeTypeId);