namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Commands;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;
using Microsoft.Extensions.Logging;

public class GetDocumentByIdSaga(
    IGetDocumentByIdCommand getDocumentByIdCommand,
    ILogger<GetDocumentsSaga> logger) : IHaveInputAndResultSaga<DocumentResult, GetDocumentByIdContext>
{
    public async Task<Option<DocumentResult>> ExecuteAsync(GetDocumentByIdContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await getDocumentByIdCommand.GetByIdAsync(context.Payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"Error happened during execution of {nameof(GetDocumentByIdSaga)}";
            throw new SagaException(m, e);
        }
    }
}