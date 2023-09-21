namespace EncyclopediaGalactica.Services.Document.Graphql.ErrorFilters;

using SourceFormatsService.Exceptions;

public class InputValidationErrorFilter :
    CustomErrorFilter<InvalidInputToDocumentServiceException>
{
}