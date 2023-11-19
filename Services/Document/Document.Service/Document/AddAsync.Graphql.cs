namespace EncyclopediaGalactica.Services.Document.Service.Document;

using Graphql.Types.Input;
using Graphql.Types.Output;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<DocumentGraphqlOutput> AddAsync(DocumentGraphqlInputType document,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}