namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Resolvers.FieldResolvers;

using Contracts.Output;
using HotChocolate.Resolvers;
using Service.Interfaces.Structure;

public class StructureNodeFieldResolvers
{
    public async Task<StructureNodeResult> GetNode(
        IResolverContext resolverContext,
        IGetStructureNodeScenario getStructureNodeScenario)
    {
        return null;
    }
}