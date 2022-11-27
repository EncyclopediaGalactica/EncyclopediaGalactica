namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.ExceptionFilters;

using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Utils.GuardsService.Exceptions;

public class ValidationExceptionsFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ArgumentNullException
            or ValidationException
            or DbUpdateException
            or GuardsServiceValueShouldNotBeEqualToException)
        {
            context.Result = new ObjectResult("Validation error")
            {
                StatusCode = (int?)HttpStatusCode.BadRequest
            };
            context.ExceptionHandled = true;
        }
    }
}