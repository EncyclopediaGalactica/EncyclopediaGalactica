namespace Document.Graphql.Resolvers.Document;

using Arguments;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using HotChocolate.Resolvers;

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
    public async Task<DocumentDto> DeleteDocumentAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        long documentId = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId);
        await documentService.DeleteAsync(documentId).ConfigureAwait(false);
        return new DocumentDto { Id = documentId };
    }
}