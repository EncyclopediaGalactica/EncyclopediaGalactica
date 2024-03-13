namespace EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.MutationResolvers;

using Arguments;
using BusinessLogic.Contracts;
using BusinessLogic.Sagas.Document;
using BusinessLogic.Sagas.Interfaces;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;

public class DocumentMutationResolvers(ILogger<DocumentMutationResolvers> logger)
{
    public async Task<DocumentResult> AddAsync(
        IResolverContext resolverContext,
        IHaveInputAndResultSaga<DocumentResult, AddDocumentSagaContext> addDocumentHaveInputAndResultSaga,
        CancellationToken cancellationToken = default)
    {
        try
        {
            AddDocumentSagaContext sagaContext = new AddDocumentSagaContext
            {
                Payload = resolverContext.ArgumentValue<DocumentInput>(ArgumentNames.Document.NewDocument)
            };
            return await addDocumentHaveInputAndResultSaga.ExecuteAsync(sagaContext, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    ///     Resolves the Delete Document mutation of the Document endpoint.
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="IResolverContext" />
    /// </param>
    /// <param name="deleteDocumentSaga"></param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(
        IResolverContext resolverContext,
        IHaveInputSaga<DeleteDocumentSagaContext> deleteDocumentSaga,
        CancellationToken cancellationToken = default)
    {
        DeleteDocumentSagaContext deleteDocumentSagaContext =
            new DeleteDocumentSagaContext
            {
                Payload = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId)
            };
        await deleteDocumentSaga.ExecuteAsync(deleteDocumentSagaContext, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    ///     Resolves the Modify Document mutation of the document endpoint.
    ///     <remarks>
    ///         It calls the <see cref="IDocumentService" /> and passes through the document identifier and the new data
    ///         in the provided <see cref="DocumentInput" /> object.
    ///     </remarks>
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="IResolverContext" />
    /// </param>
    /// <param name="updateDocumentSaga">
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation.
    /// </returns>
    public async Task<DocumentResult> UpdateDocumentAsync(
        IResolverContext resolverContext,
        IHaveInputAndResultSaga<DocumentResult, UpdateDocumentSagaContext> updateDocumentSaga,
        CancellationToken cancellationToken = default)
    {
        UpdateDocumentSagaContext context = new UpdateDocumentSagaContext
        {
            Payload = resolverContext.ArgumentValue<DocumentInput>(ArgumentNames.Document.UpdatedDocument)
        };
        return await updateDocumentSaga.ExecuteAsync(context, cancellationToken)
            .ConfigureAwait(false);
    }
}