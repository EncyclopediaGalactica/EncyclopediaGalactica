namespace EncyclopediaGalactica.Services.Document.Controllers.ExceptionFilters;

using System.Net;
using CacheService.Exceptions;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Service.Interfaces;

public class InternalServerErrorExceptionsFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is SourceFormatNodeMapperException
            or SourceFormatsCacheServiceException
            or SourceFormatNodeRepositoryException
            or DbUpdateConcurrencyException)
        {
            context.Result = new ObjectResult(SourceFormatsServiceResultStatuses.InternalError)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}