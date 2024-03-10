namespace EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.FieldResolvers;

using HotChocolate.Resolvers;

public class StructureNodeFieldResolvers(
    ILogger<StructureNodeFieldResolvers> logger)
{
    public async Task<StructureNodeResult> GetNode(
        IResolverContext resolverContext,
        IGetRootStructureNodeByDocumentIdScenario getRootStructureNodeByDocumentIdScenario)
    {
        try
        {
            DocumentResult parent = resolverContext.Parent<DocumentResult>();
            return await getRootStructureNodeByDocumentIdScenario.GetRootNodeByDocumentIdAsync(parent.Id);
        }
        catch (Exception e)
        {
            logger.LogDebug("=== ERROR ===, {Message}, {StackTrace}", e.Message, e.StackTrace);
            return default;
        }
    }
}