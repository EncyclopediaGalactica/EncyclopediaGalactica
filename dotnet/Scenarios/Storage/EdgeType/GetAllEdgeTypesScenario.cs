namespace EncyclopediaGalactica.Scenarios.Storage.EdgeType;

using Common;
using EncyclopediaGalactica.Storage;
using EncyclopediaGalactica.Storage.Repository.EdgeType;

public class GetAllEdgeTypesScenario(
    EdgeTypeRepository repository,
    StorageContext ctx
)
{
    public Either<EgError, List<EdgeTypeResult>> Execute()
        => from edgeTypes in repository.GetAll(ctx)
            from edgeTypeResults in edgeTypes.ToEdgeTypeResults()
            select edgeTypeResults;
}