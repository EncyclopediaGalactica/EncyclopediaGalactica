namespace EncyclopediaGalactica.Services.Document.Mappers.Interfaces;

using Contracts.Input;
using Entities;

/// <summary>
///     The IDocumentMapper interface
///     <remarks>
///         It provides methods to map <see cref="Document" /> to <see cref="DocumentGraphqlInput" /> and back
///     </remarks>
/// </summary>
public interface IDocumentMappers
{
    /// <summary>
    ///     Maps <see cref="DocumentGraphqlInput" /> to <see cref="Document" />
    /// </summary>
    /// <param name="graphqlInput">the input dto</param>
    /// <returns><see cref="Document" /> object</returns>
    Document MapDocumentDtoToDocument(DocumentGraphqlInput graphqlInput);

    /// <summary>
    ///     Maps <see cref="Document" /> to <see cref="DocumentGraphqlInput" />
    /// </summary>
    /// <param name="document">the input document</param>
    /// <returns>
    ///     <see cref="DocumentGraphqlInput" />
    /// </returns>
    DocumentGraphqlInput MapDocumentToDocumentDto(Document document);

    /// <summary>
    ///     Maps <see cref="List{T}" /> of <see cref="Document" /> to <see cref="List{T}" /> of
    ///     <see cref="DocumentGraphqlInput" />.
    /// </summary>
    /// <param name="l"><see cref="List{T}" /> of <see cref="Document" /></param>
    /// <returns>List of <see cref="DocumentGraphqlInput" />s.</returns>
    List<DocumentGraphqlInput> MapDocumentsToDocumentDtos(List<Document> l);
}