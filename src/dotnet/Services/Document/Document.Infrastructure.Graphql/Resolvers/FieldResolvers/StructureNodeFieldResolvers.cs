namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Resolvers.FieldResolvers;

using Contracts.Output;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;
using Scenario.Interfaces.StructureNode;

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