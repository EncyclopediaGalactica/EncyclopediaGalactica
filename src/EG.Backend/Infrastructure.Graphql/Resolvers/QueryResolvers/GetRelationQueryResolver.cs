using EncyclopediaGalactica.BusinessLogic.Contracts;
using EncyclopediaGalactica.BusinessLogic.Sagas.Interfaces;
using EncyclopediaGalactica.BusinessLogic.Sagas.Relation;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;

namespace EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.QueryResolvers;

public class GetRelationQueryResolver(ILogger<GetRelationQueryResolver> logger)
{
    public async Task<IList<RelationResult>> GetRelationAsync(
        IResolverContext resolverContext,
        IHaveInputAndResultSaga<List<RelationResult>, GetRelationSagaContext> getRelationSaga,
        CancellationToken cancellationToken = default)
    {
        return null;
    }
}
