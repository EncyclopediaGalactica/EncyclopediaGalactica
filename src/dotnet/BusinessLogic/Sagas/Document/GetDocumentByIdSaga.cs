namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Commands.Document;
using Contracts;
using Interfaces;
using Microsoft.Extensions.Logging;

public class GetDocumentByIdSaga(
    IGetDocumentByIdCommand getDocumentByIdCommand,
    ILogger<GetDocumentsSaga> logger) : IHaveInputAndResultSaga<DocumentResult, GetDocumentByIdContext>
{
    public async Task<DocumentResult> ExecuteAsync(GetDocumentByIdContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await getDocumentByIdCommand.GetByIdAsync(context.Payload, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}