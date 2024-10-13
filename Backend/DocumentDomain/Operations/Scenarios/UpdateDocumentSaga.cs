namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Commands;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;
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
    ILogger<UpdateDocumentSaga> logger)
    : IHaveInputAndResultSaga<DocumentResult, UpdateDocumentHavePayloadScenarioContext>
{
    /// <summary>
    ///     Executes the saga.
    /// </summary>
    /// <param name="context">The provided input.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Result of the saga.</returns>
    public async Task<Option<DocumentResult>> ExecuteAsync(UpdateDocumentHavePayloadScenarioContext context,
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