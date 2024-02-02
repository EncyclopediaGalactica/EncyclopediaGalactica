namespace EncyclopediaGalactica.Host.RestApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("argument")]
    public async Task<ActionResult<string>> ArgumentNull()
    {
        throw new ArgumentNullException();
    }
}