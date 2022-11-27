namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.ExceptionFilters;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class NoSuchEntityExceptionsFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is InvalidOperationException)
        {
            context.Result = new ObjectResult("No such entity")
            {
                StatusCode = (int?)HttpStatusCode.NotFound
            };

            context.ExceptionHandled = true;
        }
    }
}