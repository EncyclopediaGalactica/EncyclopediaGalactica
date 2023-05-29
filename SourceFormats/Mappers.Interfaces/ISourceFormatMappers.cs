namespace EncyclopediaGalactica.Services.Document.Mappers.Interfaces;

public interface ISourceFormatMappers
{
    ISourceFormatNodeMappers SourceFormatNodeMappers { get; }
    IDocumentMappers DocumentMappers { get; }
}