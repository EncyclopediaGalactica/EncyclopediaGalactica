namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sourceformat")]
public class BlaController : ControllerBase
{
    [HttpGet("node/get")]
    public async Task<ActionResult<List<string>>> Get()
    {
        return new List<string> { "asd", "bsd", "csd" };
    }
}