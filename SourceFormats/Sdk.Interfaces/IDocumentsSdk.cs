namespace EncyclopediaGalactica.SourceFormats.Sdk.Interfaces;

using Dtos;
using Entities;
using Models.Document;

/// <summary>
///     The IDocumentsSdk interface
///     <remarks>
///         It provides methods to access the <see cref="Document" /> entities in the system
///         using Http protocol.
///     </remarks>
/// </summary>
public interface IDocumentsSdk
{
    /// <summary>
    ///     Adds a new <see cref="Document" /> to the system where the properties of the new entity will be
    ///     based on the provided <see cref="DocumentDto" /> object's properties.
    /// </summary>
    /// <param name="model">the provided dto object</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation.
    /// </returns>
    Task<DocumentAddResponseModel> AddAsync(
        DocumentAddRequestModel model,
        CancellationToken cancellationToken = default);
}