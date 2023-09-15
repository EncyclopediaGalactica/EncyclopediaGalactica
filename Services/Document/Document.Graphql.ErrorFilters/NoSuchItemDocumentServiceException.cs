namespace Document.Graphql.ErrorFilters;

using EncyclopediaGalactica.Services.Document.SourceFormatsService.Exceptions;

public class NoSuchItemErrorFilter : CustomErrorFilter<NoSuchItemDocumentServiceException>
{
}