namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.ExceptionFilters;

using System.Net;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SourceFormatsCacheService.Exceptions;

public class InternalErrorExceptionsFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is SourceFormatNodeMapperException
            or SourceFormatsCacheServiceException
            or DbUpdateConcurrencyException)
        {
            context.Result = new ObjectResult("Internal error.")
            {
                StatusCode = (int?)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}