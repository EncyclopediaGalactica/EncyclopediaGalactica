namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Commands.Document;
using Contracts;
using Interfaces;
using Microsoft.Extensions.Logging;

/// <summary>
///     Get Document Saga.
///     <remarks>
///         Orchestrates commands to produce the list of <see cref="DocumentResult" /> objects representing
///         the list of <see cref="Document" /> entities in the system.
///     </remarks>
/// </summary>
public class GetDocumentsSaga(
    IGetAllDocumentsCommand getAllDocumentsCommand,
    ILogger<GetDocumentsSaga> logger)
    : IHaveInputAndResultSaga<List<DocumentResult>, GetDocumentsSagaContext>
{
    /// <summary>
    ///     Returns list of <see cref="DocumentResult" />.
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of the asynchronous operation.
    /// </returns>
    public async Task<List<DocumentResult>> ExecuteAsync(
        GetDocumentsSagaContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await getAllDocumentsCommand.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}