namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Commands.Document;
using Commands.StructureNode;
using Interfaces;

/// <inheritdoc />
public class AddDocumentSaga(
    IAddDocumentCommand addDocumentCommand,
    IAddStructureNodeTreeCommand addStructureNodeTreeCommand,
    IGetDocumentByIdCommand getDocumentByIdCommand,
    ILogger<AddDocumentSaga> logger) : IHaveInputAndResultSaga<DocumentResult, AddDocumentSagaContext>
{
    /// <summary>
    ///     Executes the saga
    /// </summary>
    /// <param name="context">Context including the <see cref="TPayloadType" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>
    ///     <see cref="TReturnType" />
    /// </returns>
    public async Task<DocumentResult> ExecuteAsync(AddDocumentSagaContext context,
        CancellationToken cancellationToken = default)
    {
        long documentId = await addDocumentCommand.AddAsync(context.Payload, cancellationToken)
            .ConfigureAwait(false);
        if (context.Payload.RootStructureNode is not null)
        {
            await addStructureNodeTreeCommand.AddTreeAsync(context.Payload.RootStructureNode, cancellationToken)
                .ConfigureAwait(false);
        }

        DocumentResult result = await getDocumentByIdCommand.GetByIdAsync(documentId, cancellationToken)
            .ConfigureAwait(false);
        return result;
    }
}