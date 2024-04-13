namespace EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.FieldResolvers;

using BusinessLogic.Commands.StructureNode;
using BusinessLogic.Contracts;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;

public class StructureNodeFieldResolvers(ILogger<StructureNodeFieldResolvers> logger)
{
    public async Task<StructureNodeResult> GetNodeAsync(
        IResolverContext resolverContext,
        IGetStructureNodeTreeCommand getStructureNodeTreeCommand,
        CancellationToken cancellationToken = default)
    {
        try
        {
            DocumentResult parent = resolverContext.Parent<DocumentResult>();
            return await getStructureNodeTreeCommand.GetRootNodeByDocumentIdAsync(parent.Id, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            logger.LogDebug("=== ERROR ===, {Message}, {StackTrace}", e.Message, e.StackTrace);
            return default;
        }
    }
}