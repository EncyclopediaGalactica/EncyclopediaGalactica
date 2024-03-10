namespace EncyclopediaGalactica.BusinessLogic.Sagas.Document;

using Commands.Document;
using Interfaces;

public class DeleteDocumentSaga(
    IDeleteDocumentCommand deleteDocumentCommand,
    ILogger<DeleteDocumentSaga> logger) : IHaveInputSaga<DeleteDocumentSagaContext>
{
    public async Task ExecuteAsync(DeleteDocumentSagaContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await deleteDocumentCommand.DeleteAsync(context.Payload, cancellationToken).ConfigureAwait(false);
            // todo clean up structure nodes
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}