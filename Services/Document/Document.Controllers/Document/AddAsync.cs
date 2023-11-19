namespace EncyclopediaGalactica.Services.Document.Controllers.Document;

using System.Net.Mime;
using Contracts.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public partial class DocumentController
{
    [HttpPost]
    [Route("add")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(DocumentGraphqlInput), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DocumentGraphqlInput>> AddAsync(DocumentGraphqlInput graphqlInput)
    {
        return await _sourceFormatsService.DocumentService.AddAsync(graphqlInput).ConfigureAwait(false);
    }
}