namespace EncyclopediaGalactica.SourceFormats.ExceptionFilters;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SourceFormatsService.Exceptions;

public class SourceFormatNodeServiceInputValidationExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is SourceFormatNodeServiceInputValidationException exception)
        {
            context.Result = new ObjectResult(exception.Message)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }
    }

    public int Order => Int32.MaxValue - 10;
}