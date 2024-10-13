namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Controllers;

using EncyclopediaGalactica.Common.Contracts;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Operations.Scenarios.DocumentType;

[ApiController]
[Route("api/document")]
public class DocumentTypeController(
    GetDocumentTypesScenario getDocumentTypesScenario,
    AddDocumentTypeScenario addDocumentTypeScenario,
    UpdateDocumentTypeScenario updateDocumentTypeScenario
)
{
    [HttpGet]
    [Route("documentType")]
    public async Task<IActionResult> GetDocumentTypesAsync(CancellationToken cancellationToken = default)
    {
        GetDocumentTypesScenarioContext ctx = new GetDocumentTypesScenarioContext();
        Option<List<DocumentTypeResult>> result = await getDocumentTypesScenario.ExecuteAsync(
            ctx,
            cancellationToken).ConfigureAwait(false);
        if (result.IsSome)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestResult();
    }

    [HttpPost]
    [Route("documnetType")]
    public async Task<IActionResult> AddDocumnetTypeAsync(
        [FromBody]
        DocumentTypeInput input,
        CancellationToken cancellationToken = default)
    {
        AddDocumentTypeScenarioContext ctx = new AddDocumentTypeScenarioContext
        {
            Payload = input
        };
        Option<DocumentTypeResult> result = await addDocumentTypeScenario.ExecuteAsync(ctx, cancellationToken)
            .ConfigureAwait(false);

        if (result.IsNone)
        {
            return new BadRequestResult();
        }

        return new OkObjectResult(result.IfNone(new DocumentTypeResult()));
    }

    [HttpPut]
    [Route("documentType/{documentTypeId}")]
    public async Task<IActionResult> UpdateDocumentTypeAsync(
        [FromBody]
        DocumentTypeInput input,
        long documentTypeId,
        CancellationToken cancellationToken = default)
    {
        UpdateDocumentTypeScenarioContext ctx = new UpdateDocumentTypeScenarioContext
        {
            Payload = input
        };
        Option<DocumentTypeResult> result = await updateDocumentTypeScenario.ExecuteAsync(
            ctx,
            cancellationToken).ConfigureAwait(false);

        if (result.IsNone)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(result.IfNone(new DocumentTypeResult()));
    }
}