namespace EncyclopediaGalactica.Infrastructure.Graphql.ErrorFilters;

public class InputValidationErrorFilter :
    CustomErrorFilter<InvalidInputToDocumentServiceException>
{
}