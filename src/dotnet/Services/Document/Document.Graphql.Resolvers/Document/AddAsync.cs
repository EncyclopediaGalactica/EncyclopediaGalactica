namespace EncyclopediaGalactica.Services.Document.Graphql.Resolvers.Document;

using Contracts.Input;
using Contracts.Output;
using HotChocolate.Resolvers;
using Service.Interfaces.Document;

public partial class DocumentResolvers
{
    public async Task<DocumentResult> AddAsync(
        IResolverContext resolverContext,
        IAddDocumentScenario addDocumentScenario)
    {
        DocumentInput newDocumentInputType = resolverContext.ArgumentValue<DocumentInput>("newDocument");
        return await addDocumentScenario.AddAsync(newDocumentInputType);
    }
}