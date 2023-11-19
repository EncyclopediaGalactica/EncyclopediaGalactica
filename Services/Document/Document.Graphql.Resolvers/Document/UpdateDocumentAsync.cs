namespace EncyclopediaGalactica.Services.Document.Graphql.Resolvers.Document;

using Arguments;
using Contracts.Input;
using HotChocolate.Resolvers;
using Service.Interfaces.Document;

public partial class DocumentResolvers
{
    /// <summary>
    ///     Resolves the Modify Document mutation of the document endpoint.
    ///     <remarks>
    ///         It calls the <see cref="IDocumentService" /> and passes through the document identifier and the new data
    ///         in the provided <see cref="DocumentGraphqlInput" /> object.
    ///     </remarks>
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="IResolverContext" />
    /// </param>
    /// <param name="documentService">
    ///     <see cref="IDocumentService" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation.
    /// </returns>
    public async Task<DocumentGraphqlInput> UpdateDocumentAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        long documentId = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId);
        DocumentGraphqlInput modifiedGraphqlInput =
            resolverContext.ArgumentValue<DocumentGraphqlInput>(ArgumentNames.Document.UpdatedDocument);
        return await documentService.UpdateAsync(documentId, modifiedGraphqlInput).ConfigureAwait(false);
    }
}