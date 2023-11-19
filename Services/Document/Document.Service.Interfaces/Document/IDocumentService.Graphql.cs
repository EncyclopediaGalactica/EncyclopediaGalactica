namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Document;

using Exceptions;
using Graphql.Types.Input;
using Graphql.Types.Output;

public partial interface IDocumentService
{
    /// <summary>
    ///     Adds a <see cref="Document" /> object to the system with the values represented by the provided
    ///     <see cref="DocumentGraphqlInputType" />.
    /// </summary>
    /// <param name="document">The input object</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns a <see cref="Task{TResult}" /> object representing the result of an asynchronous operation.
    /// </returns>
    /// <exception cref="InvalidInputToDocumentServiceException">
    ///     When invalid input is provided to the service
    /// </exception>
    /// <exception cref="DocumentServiceOperationCancelledException">
    ///     When the operation cancelled by <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="UnknownErrorDocumentServiceException">
    ///     When any other error happens.
    /// </exception>
    Task<DocumentGraphqlOutput> AddAsync(
        DocumentGraphqlInputType document,
        CancellationToken cancellationToken = default);
}