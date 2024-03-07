namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.ErrorFilters;

using Scenario.Exceptions;

public class InputValidationErrorFilter :
    CustomErrorFilter<InvalidInputToDocumentServiceException>
{
}