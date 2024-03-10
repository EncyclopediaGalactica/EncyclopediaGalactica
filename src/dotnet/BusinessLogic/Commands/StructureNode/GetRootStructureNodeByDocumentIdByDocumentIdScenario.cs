namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using System.Text;
using Exceptions;
using Mappers;

public class GetRootStructureNodeByDocumentIdByDocumentIdScenario(
    IGuardsService guardsService,
    IStructureNodeRepository repository,
    IStructureNodeMappers mappers,
    ILogger<IGetRootStructureNodeByDocumentIdScenario> logger) : IGetRootStructureNodeByDocumentIdScenario
{
    public async Task<StructureNodeResult> GetRootNodeByDocumentIdAsync(long documentId)
    {
        try
        {
            return await GetNodeBusinessLogicAsync(documentId).ConfigureAwait(false);
        }
        catch (Exception e) when (e is ArgumentNullException or GuardsServiceException)
        {
            string m =
                $"Invalid arguments was passed to {nameof(GetRootStructureNodeByDocumentIdByDocumentIdScenario)}. ";
            throw new InvalidArgumentCommandException(m, e);
        }
        catch (Exception e) when (e is InvalidOperationException)
        {
            string m = $"There is no item matches for expectations. ";
            throw new NoSuchItemCommandException(m, e);
        }
        catch (Exception e) when (e is OperationCanceledException)
        {
            string m = $"The operation was cancelled. ";
            throw new OperationCancelledScenarioException(m, e);
        }
        catch (Exception e) when (e is DataCohesionEncyclopediaGalacticaException)
        {
            string m = "Data is corrupted.";
            throw new DataCohesionScenarioException(m, e);
        }
        catch (Exception e)
        {
            string m = $"Unknown error happened!";
            throw new UnknownErrorScenarioException(m, e);
        }
    }

    private async Task<StructureNodeResult> GetNodeBusinessLogicAsync(long documentId)
    {
        ValidateInput(documentId);
        List<StructureNode> structureNodes =
            await repository.GetRootNodesByDocumentIdAsync(documentId).ConfigureAwait(false);
        DoesDocumentHaveASingleRootNode(documentId, structureNodes);
        return mappers.MapStructureNodeToStructureNodeResult(structureNodes.First());
    }

    private void DoesDocumentHaveASingleRootNode(long documentId, List<StructureNode> nodes)
    {
        if (nodes.Any() && nodes.Count > 1)
        {
            StringBuilder builder = new StringBuilder($"Document with id: {documentId} has multiple root nodes.");
            builder.Append("Structure node ids marked as root nodes: ");

            nodes.ForEach(item => { builder.Append(item.Id).Append(" "); });

            throw new DataCohesionEncyclopediaGalacticaException(builder.ToString());
        }
    }

    private void ValidateInput(long id)
    {
        guardsService.IsNotEqual(id, 0);
    }
}