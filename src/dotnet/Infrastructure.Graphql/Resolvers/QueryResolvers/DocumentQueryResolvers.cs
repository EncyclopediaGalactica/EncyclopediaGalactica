namespace EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.QueryResolvers;

using Arguments;
using BusinessLogic.Contracts;
using BusinessLogic.Sagas.Document;
using BusinessLogic.Sagas.Interfaces;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;

public class DocumentQueryResolvers(ILogger<DocumentQueryResolvers> logger)
{
    /// <summary>
    ///     Returns a list of <see cref="DocumentInput" /> representing <see cref="resolverContext" /> entities of the
    ///     system.
    /// </summary>
    /// <param name="resolverContext">
    /// </param>
    /// <param name="getDocumentsSaga"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    public async Task<IList<DocumentResult>> GetAllAsync(
        IResolverContext resolverContext,
        IHaveInputAndResultSaga<List<DocumentResult>, GetDocumentsSagaContext> getDocumentsSaga,
        CancellationToken cancellationToken = default)
    {
        try
        {
            GetDocumentsSagaContext sagaContext = new GetDocumentsSagaContext();
            return await getDocumentsSaga.ExecuteAsync(sagaContext, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            logger.LogDebug("{OperationName} has failed. Message: {Message}; Stacktrace: {StackTrace}",
                nameof(GetAllAsync),
                e.Message,
                e.StackTrace);
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<DocumentResult> GetByIdAsync(
        IResolverContext resolverContext,
        IHaveInputAndResultSaga<DocumentResult, GetDocumentByIdContext> getDocumentByIdSaga,
        CancellationToken cancellationToken = default)
    {
        try
        {
            GetDocumentByIdContext context = new GetDocumentByIdContext
            {
                Payload = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId)
            };
            return await getDocumentByIdSaga.ExecuteAsync(context, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}