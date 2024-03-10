namespace EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.MutationResolvers;

using Arguments;
using HotChocolate.Resolvers;

public class DocumentMutationResolvers(ILogger<DocumentMutationResolvers> logger)
{
    public async Task<DocumentResult> AddAsync(
        IResolverContext resolverContext,
        IHaveInputAndResultSaga<DocumentResult, DocumentInput> addDocumentHaveInputAndResultSaga)
    {
        try
        {
            AddDocumentSagaContext sagaContext = new AddDocumentSagaContext
            {
                Payload = resolverContext.ArgumentValue<DocumentInput>(ArgumentNames.Document.NewDocument)
            };
            return await addDocumentHaveInputAndResultSaga.ExecuteAsync(sagaContext.Payload).ConfigureAwait(false);
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
    /// <param name="deleteDocumentCommand">
    ///     <see cref="IDocumentService" />
    /// </param>
    public async Task DeleteAsync(
        IResolverContext resolverContext,
        IDeleteDocumentCommand deleteDocumentCommand)
    {
        DeleteDocumentSagaContext deleteDocumentSagaContext = new DeleteDocumentSagaContext
        {
            Payload = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId)
        };
        await deleteDocumentCommand.DeleteAsync(deleteDocumentSagaContext.Payload).ConfigureAwait(false);
    }

    /// <summary>
    ///     Returns a list of <see cref="DocumentInput" /> representing <see cref="resolverContext" /> entities of the
    ///     system.
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="getAllDocumentsCommand" />
    /// </param>
    /// <param name="getAllDocumentsCommand">
    ///     <see cref="Task{TResult}" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Document" /> representing result of asynchronous operation.
    /// </returns>
    public async Task<IList<DocumentResult>> GetAllAsync(
        IResolverContext resolverContext,
        IGetAllDocumentsCommand getAllDocumentsCommand)
    {
        try
        {
            return await getAllDocumentsCommand.GetAllAsync();
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
    /// <param name="updateDocumentHaveInputAndResultSaga">
    ///     <see cref="IDocumentService" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation.
    /// </returns>
    public async Task<DocumentResult> UpdateDocumentAsync(
        IResolverContext resolverContext,
        IHaveInputAndResultSaga<DocumentResult, DocumentInput> updateDocumentHaveInputAndResultSaga)
    {
        UpdateDocumentSagaContext context = new UpdateDocumentSagaContext
        {
            Payload = resolverContext.ArgumentValue<DocumentInput>(ArgumentNames.Document.UpdatedDocument)
        };
        return await updateDocumentHaveInputAndResultSaga.ExecuteAsync(context.Payload).ConfigureAwait(false);
    }
}