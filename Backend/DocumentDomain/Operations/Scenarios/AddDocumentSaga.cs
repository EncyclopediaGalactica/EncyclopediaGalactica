namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Commands;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;
using Microsoft.Extensions.Logging;

/// <inheritdoc />
public class AddDocumentSaga(
    IAddDocumentCommand addDocumentCommand,
    IAddStructureNodeTreeCommand addStructureNodeTreeCommand,
    IGetDocumentByIdCommand getDocumentByIdCommand,
    ILogger<AddDocumentSaga> logger) : IHaveInputAndResultSaga<DocumentResult, AddDocumentHavePayloadScenarioContext>
{
    /// <summary>
    ///     Executes the saga
    /// </summary>
    /// <param name="context">Context including the <see cref="TPayloadType" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>
    ///     <see cref="TReturnType" />
    /// </returns>
    public async Task<Option<DocumentResult>> ExecuteAsync(AddDocumentHavePayloadScenarioContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            long documentId = await addDocumentCommand.AddAsync(context.Payload, cancellationToken)
                .ConfigureAwait(false);

            DocumentResult result = await getDocumentByIdCommand.GetByIdAsync(documentId, cancellationToken)
                .ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            string m = $"Error happened while executing {nameof(AddDocumentSaga)}.";
            throw new SagaException(m, e);
        }
    }
}