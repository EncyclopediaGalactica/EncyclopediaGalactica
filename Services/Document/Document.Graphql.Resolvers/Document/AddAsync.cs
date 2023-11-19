namespace EncyclopediaGalactica.Services.Document.Graphql.Resolvers.Document;

using HotChocolate.Resolvers;
using Service.Interfaces.Document;
using Types.Input;
using Types.Output;

public partial class DocumentResolvers
{
    public async Task<DocumentGraphqlOutput> AddAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        DocumentGraphqlInputType newDocumentGraphqlInputType =
            resolverContext.ArgumentValue<DocumentGraphqlInputType>("newDocument");
        return await documentService.AddAsync(newDocumentGraphqlInputType);
    }
}