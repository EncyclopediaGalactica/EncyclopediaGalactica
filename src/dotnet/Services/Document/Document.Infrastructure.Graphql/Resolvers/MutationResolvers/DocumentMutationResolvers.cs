namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Resolvers.MutationResolvers;

using Arguments;
using Contracts.Input;
using Contracts.Output;
using EncyclopediaGalactica.Document.Saga.Interfaces;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;
using Scenario.Interfaces.Document;

public class DocumentMutationResolvers(ILogger<DocumentMutationResolvers> logger)
{
    public async Task<DocumentResult> AddAsync(
        IResolverContext resolverContext,
        IAddDocumentSaga addDocumentSaga)
    {
        try
        {
            DocumentInput newDocumentInputType =
                resolverContext.ArgumentValue<DocumentInput>(ArgumentNames.Document.NewDocument);
            return await addDocumentSaga.AddAsync(newDocumentInputType);
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
    /// <param name="deleteDocumentScenario">
    ///     <see cref="IDocumentService" />
    /// </param>
    public async Task<DocumentInput> DeleteAsync(
        IResolverContext resolverContext,
        IDeleteDocumentScenario deleteDocumentScenario)
    {
        long documentId = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId);
        await deleteDocumentScenario.DeleteAsync(documentId).ConfigureAwait(false);
        return new DocumentInput { Id = documentId };
    }

    /// <summary>
    ///     Returns a list of <see cref="DocumentInput" /> representing <see cref="resolverContext" /> entities of the
    ///     system.
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="getAllDocumentsScenario" />
    /// </param>
    /// <param name="getAllDocumentsScenario">
    ///     <see cref="Task{TResult}" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Document" /> representing result of asynchronous operation.
    /// </returns>
    public async Task<IList<DocumentResult>> GetAllAsync(
        IResolverContext resolverContext,
        IGetAllDocumentsScenario getAllDocumentsScenario)
    {
        try
        {
            return await getAllDocumentsScenario.GetAllAsync();
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
    /// <param name="updateDocumentScenario">
    ///     <see cref="IDocumentService" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation.
    /// </returns>
    public async Task<DocumentResult> UpdateDocumentAsync(
        IResolverContext resolverContext,
        IUpdateDocumentScenario updateDocumentScenario)
    {
        long documentId = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId);
        DocumentInput modifiedInput =
            resolverContext.ArgumentValue<DocumentInput>(ArgumentNames.Document.UpdatedDocument);
        return await updateDocumentScenario.UpdateAsync(documentId, modifiedInput).ConfigureAwait(false);
    }
}