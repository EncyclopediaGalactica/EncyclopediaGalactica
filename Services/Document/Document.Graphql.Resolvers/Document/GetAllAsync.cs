namespace Document.Graphql.Resolvers.Document;

using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using HotChocolate.Resolvers;

public partial class DocumentResolvers
{
    /// <summary>
    ///     Returns a list of <see cref="DocumentDto" /> representing <see cref="resolverContext" /> entities of the system.
    /// </summary>
    /// <param name="resolverContext">
    ///     <see cref="documentService" />
    /// </param>
    /// <param name="documentService">
    ///     <see cref="Task{TResult}" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Document" /> representing result of asynchronous operation.
    /// </returns>
    public async Task<IList<DocumentDto>> GetAllAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        return await documentService.GetAllAsync();
    }
}