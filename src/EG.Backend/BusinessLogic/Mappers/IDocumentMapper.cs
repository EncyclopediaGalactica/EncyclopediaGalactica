namespace EncyclopediaGalactica.BusinessLogic.Mappers;

using Contracts;
using Entities;

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
    ///     Maps <see cref="DocumentInput" /> to <see cref="Document" />
    /// </summary>
    /// <param name="input">the input dto</param>
    /// <returns><see cref="Document" /> object</returns>
    Document MapDocumentInputToDocument(DocumentInput input);

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