namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;
using Entities;
using Exceptions;
using Mappers;
using Microsoft.Extensions.Logging;

public class GetStructureNodeTreeCommand(
    IStructureNodeMapper mapper,
    ILogger<IGetStructureNodeTreeCommand> logger) : IGetStructureNodeTreeCommand
{
    public async Task<StructureNodeResult> GetRootNodeByDocumentIdAsync(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetNodeBusinessLogicAsync(documentId, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Unknown error happened!";
            throw new UnknownErrorScenarioException(m, e);
        }
    }

    private async Task<StructureNodeResult> GetNodeBusinessLogicAsync(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        ValidateInput(documentId);
        StructureNode structureNode = await GetNodeTreeAsyncDatabaseOperation(documentId, cancellationToken)
            .ConfigureAwait(false);
        return mapper.MapStructureNodeToStructureNodeResult(structureNode);
    }

    private async Task<StructureNode> GetNodeTreeAsyncDatabaseOperation(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        return new StructureNode();
    }

    private void ValidateInput(long id)
    {
        long notAllowedValue = 0;
        if (id == notAllowedValue)
        {
            string m = $"{nameof(id)} cannot be {notAllowedValue}";
            throw new InvalidArgumentCommandException(m);
        }
    }
}