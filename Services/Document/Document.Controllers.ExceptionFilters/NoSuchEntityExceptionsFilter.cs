namespace EncyclopediaGalactica.Services.Document.Controllers.ExceptionFilters;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Interfaces;

public class NoSuchEntityExceptionsFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is InvalidOperationException)
        {
            context.Result = new ObjectResult(SourceFormatsServiceResultStatuses.NoSuchEntity)
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };

            context.ExceptionHandled = true;
        }
    }
}