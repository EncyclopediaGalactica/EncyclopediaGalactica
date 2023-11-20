namespace EncyclopediaGalactica.Services.Document.Graphql.Resolvers.Document;

using Arguments;
using Contracts.Input;
using HotChocolate.Resolvers;
using Service.Interfaces.Document;

public partial class DocumentResolvers
{
    /// <summary>
    ///     Resolves the Delete Document mutation of the Document endpoint.
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="IResolverContext" />
    /// </param>
    /// <param name="documentService">
    ///     <see cref="IDocumentService" />
    /// </param>
    public async Task<DocumentInput> DeleteDocumentAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        long documentId = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId);
        await documentService.DeleteAsync(documentId).ConfigureAwait(false);
        return new DocumentInput { Id = documentId };
    }
}