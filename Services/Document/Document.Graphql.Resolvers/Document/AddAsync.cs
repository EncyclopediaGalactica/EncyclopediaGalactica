namespace Document.Graphql.Resolvers.Document;

using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using HotChocolate.Resolvers;

public partial class DocumentResolvers
{
    public async Task<DocumentDto> AddAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        DocumentDto newDocumentInput = resolverContext.ArgumentValue<DocumentDto>("newDocument");
        return await documentService.AddAsync(newDocumentInput);
    }
}