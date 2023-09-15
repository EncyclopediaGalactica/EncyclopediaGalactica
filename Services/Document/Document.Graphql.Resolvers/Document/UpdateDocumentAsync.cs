namespace Document.Graphql.Resolvers.Document;

using Arguments;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using HotChocolate.Resolvers;

public partial class DocumentResolvers
{
    /// <summary>
    ///     Resolves the Modify Document mutation of the document endpoint.
    ///     <remarks>
    ///         It calls the <see cref="IDocumentService" /> and passes through the document identifier and the new data
    ///         in the provided <see cref="DocumentDto" /> object.
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
    public async Task<DocumentDto> UpdateDocumentAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        long documentId = resolverContext.ArgumentValue<long>(ArgumentNames.Document.DocumentId);
        DocumentDto modifiedDto = resolverContext.ArgumentValue<DocumentDto>(ArgumentNames.Document.UpdatedDocument);
        return await documentService.UpdateAsync(documentId, modifiedDto).ConfigureAwait(false);
    }
}