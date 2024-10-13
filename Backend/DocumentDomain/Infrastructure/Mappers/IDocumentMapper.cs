namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;

using EncyclopediaGalactica.Common.Contracts;
using Entity;

/// <summary>
///     The IDocumentMapper interface
///     <remarks>
///         It provides methods to map between <see cref="Document" />, <see cref="DocumentResult" /> and
///         <see cref="DocumentInput" />.
///     </remarks>
/// </summary>
public interface IDocumentMapper
{
    /// <summary>
    ///     Maps <see cref="Document" /> to <see cref="DocumentResult" />
    /// </summary>
    /// <param name="document">the input document</param>
    /// <returns>
    ///     <see cref="DocumentInput" />
    /// </returns>
    DocumentResult MapDocumentToDocumentResult(Document document);

    /// <summary>
    ///     Maps <see cref="List{T}" /> of <see cref="Document" /> to <see cref="List{T}" /> of
    ///     <see cref="DocumentResult" />.
    /// </summary>
    /// <param name="l"><see cref="List{T}" /> of <see cref="Document" /></param>
    /// <returns>List of <see cref="DocumentResult" />s.</returns>
    List<DocumentResult> MapDocumentsToDocumentResults(List<Document> l);
}