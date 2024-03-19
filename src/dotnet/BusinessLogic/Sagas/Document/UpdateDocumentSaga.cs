namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Commands.Document;
using Contracts;
using Interfaces;
using Microsoft.Extensions.Logging;

/// <summary>
///     Update A Document Saga.
///     <remarks>
///         Orchestrates commands to update the designated <see cref="Document" /> entity in the system based on the
///         provided input.
///     </remarks>
/// </summary>
/// <param name="updateDocumentCommand"><see cref="IUpdateDocumentCommand" />.</param>
/// <param name="getDocumentByIdCommand"><see cref="IGetDocumentByIdCommand" />.</param>
/// <param name="logger">Logger.</param>
public class UpdateDocumentSaga(
    IUpdateDocumentCommand updateDocumentCommand,
    IGetDocumentByIdCommand getDocumentByIdCommand,
    ILogger<UpdateDocumentSaga> logger) : IHaveInputAndResultSaga<DocumentResult, UpdateDocumentSagaContext>
{
    /// <summary>
    ///     Executes the saga.
    /// </summary>
    /// <param name="context">The provided input.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Result of the saga.</returns>
    public async Task<DocumentResult> ExecuteAsync(UpdateDocumentSagaContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await updateDocumentCommand.UpdateAsync(context.Payload, cancellationToken)
                .ConfigureAwait(false);
            return await getDocumentByIdCommand.GetByIdAsync(context.Payload.Id, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during execution of {nameof(UpdateDocumentSaga)}.";
            throw new SagaException(m, e);
        }
    }
}