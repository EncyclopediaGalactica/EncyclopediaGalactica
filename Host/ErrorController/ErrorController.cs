namespace EncyclopediaGalactica.SourceFormats.Host.ErrorController;

using Microsoft.AspNetCore.Mvc;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError() => Problem();
}