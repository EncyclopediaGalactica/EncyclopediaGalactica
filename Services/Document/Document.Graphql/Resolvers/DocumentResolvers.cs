namespace Document.Graphql.Resolvers;

using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;
using HotChocolate.Resolvers;

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
        DocumentDto result = await documentService.AddAsync(newDocumentInput);
        return result;
    }
}