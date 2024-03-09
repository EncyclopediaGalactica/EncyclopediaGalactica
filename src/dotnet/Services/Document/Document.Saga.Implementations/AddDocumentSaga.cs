namespace EncyclopediaGalactica.Document.Saga.Implementations;

using Interfaces;
using Microsoft.Extensions.Logging;
using Services.Document.Contracts.Input;
using Services.Document.Contracts.Output;
using Services.Document.Scenario.Interfaces.Document;

public class AddDocumentSaga(
    IAddDocumentScenario addDocumentScenario,
    ILogger<AddDocumentSaga> logger) : IAddDocumentSaga
{
    public async Task<DocumentResult> AddAsync(DocumentInput documentInput,
        CancellationToken cancellationToken = default)
    {
        long documentId = await addDocumentScenario.AddAsync(documentInput).ConfigureAwait(false);
    }
}