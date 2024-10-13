namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Controllers;

using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Operations.Scenarios;

[ApiController]
[Route("api/document")]
public class DocumentController(
    GetDocumentsSaga getDocumentsSaga)
{
    [HttpGet]
    [Route("getDocuments")]
    public async Task<IActionResult> GetDocumentsAsync()
    {
        GetDocumentsSagaContext ctx = new GetDocumentsSagaContext();
        Option<List<DocumentResult>> result = await getDocumentsSaga.ExecuteAsync(ctx).ConfigureAwait(false);

        return new OkObjectResult(result
            .IfNone(new List<DocumentResult>()));
    }
}