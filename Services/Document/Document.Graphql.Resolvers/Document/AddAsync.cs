namespace EncyclopediaGalactica.Services.Document.Graphql.Resolvers.Document;

using Dtos;
using HotChocolate.Resolvers;
using Service.Interfaces.Document;

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