namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Resolvers;

using EncyclopediaGalactica.Common.Contracts;
using HotChocolate.Resolvers;
using LanguageExt;
using Microsoft.Extensions.Logging;
using Operations.Scenarios.DocumentType;

public class DocumentTypeResolver(
    ILogger<DocumentTypeResolver> logger)
{
    public async Task<List<DocumentTypeResult>> GetDocumentTypes(
        [Service]
        GetDocumentTypesScenario getDocumentTypesScenario,
        CancellationToken cancellationToken)

    {
        GetDocumentTypesScenarioContext ctx = new GetDocumentTypesScenarioContext();
        Option<List<DocumentTypeResult>>
            result = await getDocumentTypesScenario.ExecuteAsync(ctx).ConfigureAwait(false);

        return result.Match(
            Some: v => v,
            None: () => []);
    }

    public async Task<DocumentTypeResult> GetDocumentTypeById(
        long id,
        [Service]
        GetDocumentTypeByIdScenario getDocumentTypeByIdScenario)
    {
        GetDocumentTypeByIdScenarioContext ctx = new GetDocumentTypeByIdScenarioContext
        {
            Payload = id,
            CorrelationId = Guid.NewGuid()
        };
        Option<DocumentTypeResult> result = await getDocumentTypeByIdScenario.ExecuteAsync(ctx).ConfigureAwait(false);
        return result.Match(
            Some: v => v,
            None: new DocumentTypeResult());
    }

    public async Task<DocumentTypeResult> AddDocumentType(
        IResolverContext resolverContext,
        [Service]
        AddDocumentTypeScenario addDocumentTypeScenario,
        CancellationToken cancellationToken)
    {
        AddDocumentTypeScenarioContext ctx = new AddDocumentTypeScenarioContext
        {
            CorrelationId = Guid.NewGuid(),
            Payload = resolverContext.ArgumentValue<DocumentTypeInput>("input")
        };
        Option<DocumentTypeResult> result = await addDocumentTypeScenario.ExecuteAsync(ctx, cancellationToken)
            .ConfigureAwait(false);
        return result.Match(
            Some: res => res,
            None: new DocumentTypeResult());
    }
}