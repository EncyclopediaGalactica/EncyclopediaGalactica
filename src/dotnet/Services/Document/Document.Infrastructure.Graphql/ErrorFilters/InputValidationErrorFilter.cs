namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.ErrorFilters;

using EncyclopediaGalactica.Services.Document.Service.Exceptions;

public class InputValidationErrorFilter :
    CustomErrorFilter<InvalidInputToDocumentServiceException>
{
}