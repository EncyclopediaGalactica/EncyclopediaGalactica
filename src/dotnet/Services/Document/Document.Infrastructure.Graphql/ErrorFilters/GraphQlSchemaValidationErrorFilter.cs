namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.ErrorFilters;

using HotChocolate;

public class GraphQlSchemaValidationErrorFilter : IErrorFilter
{
    private const string ValidationErrorMessage = "GraphQl schema validation error!";

    public IError OnError(IError error)
    {
        error = HuntingForGraphqlSchemaViolationErrors(error);
        return error;
    }

    private IError HuntingForGraphqlSchemaViolationErrors(IError error)
    {
        if (error.Exception == null
            && error.Code == "HC0018")
        {
            error = RemoveSensitiveInformation(error);
            return error.WithMessage(Errors.Errors.InvalidInput);
        }

        return error;
    }

    private IError RemoveSensitiveInformation(IError error)
    {
        return error.RemoveExtension("field")
            .RemoveExtension("code")
            .RemoveLocations()
            .RemovePath();
    }
}