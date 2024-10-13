namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;

using EncyclopediaGalactica.Common.Contracts;
using Entity;

public interface IDocumentInputMapper
{
    /// <summary>
    ///     Maps <see cref="DocumentInput" /> to <see cref="Document" />
    /// </summary>
    /// <param name="input">the input dto</param>
    /// <returns><see cref="Document" /> object</returns>
    Document MapDocumentInputToDocument(DocumentInput input);
}