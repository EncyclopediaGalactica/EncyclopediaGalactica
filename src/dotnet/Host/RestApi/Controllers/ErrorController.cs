namespace EncyclopediaGalactica.Host.RestApi.Controllers;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("error-development")]
    public ActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        IExceptionHandlerFeature exceptionHandleFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        return Problem(
            detail: exceptionHandleFeature.Error.StackTrace,
            title: exceptionHandleFeature.Error.Message);
    }

    [Route("error")]
    public IActionResult HandleError() => Problem();
}