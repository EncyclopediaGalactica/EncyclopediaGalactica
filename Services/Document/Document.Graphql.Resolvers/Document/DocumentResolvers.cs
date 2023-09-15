namespace Document.Graphql.Resolvers.Document;

using Microsoft.Extensions.Logging;

public partial class DocumentResolvers
{
    private readonly ILogger<DocumentResolvers> _logger;

    public DocumentResolvers(ILogger<DocumentResolvers> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }
}