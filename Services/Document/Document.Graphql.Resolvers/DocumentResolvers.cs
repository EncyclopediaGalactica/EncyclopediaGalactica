namespace Document.Graphql.Resolvers;

using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using HotChocolate.Resolvers;
using Microsoft.Extensions.Logging;

public class DocumentResolvers
{
    private readonly ILogger<DocumentResolvers> _logger;

    public DocumentResolvers(ILogger<DocumentResolvers> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }

    public async Task<DocumentDto> AddAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        DocumentDto newDocumentInput = resolverContext.ArgumentValue<DocumentDto>("newDocument");
        return await documentService.AddAsync(newDocumentInput);
    }

    public async Task<IList<DocumentDto>> GetAllAsync(
        IResolverContext resolverContext,
        IDocumentService documentService)
    {
        return await documentService.GetAllAsync();
    }
}