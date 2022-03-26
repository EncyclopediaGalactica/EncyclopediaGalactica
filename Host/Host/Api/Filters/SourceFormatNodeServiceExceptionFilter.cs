namespace Host.Api.Filters;

using EncyclopediaGalactica.SourceFormats.SourceFormatsService.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SourceFormatNodeServiceExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is SourceFormatNodeServiceException exception)
        {
            context.Result = new ObjectResult(exception)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }

    public int Order => Int32.MaxValue - 10;
}