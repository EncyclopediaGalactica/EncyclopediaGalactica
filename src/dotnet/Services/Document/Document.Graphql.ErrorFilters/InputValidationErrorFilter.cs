namespace EncyclopediaGalactica.Services.Document.Graphql.ErrorFilters;

using Service.Exceptions;

public class InputValidationErrorFilter :
    CustomErrorFilter<InvalidInputToDocumentServiceException>
{
}