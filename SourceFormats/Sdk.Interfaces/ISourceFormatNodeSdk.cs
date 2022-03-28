namespace Sdk.Interfaces;

using EncyclopediaGalactica.SourceFormats.Dtos;
using Exceptions;

public interface ISourceFormatNodeSdk
{
    Task<SourceFormatNodeDto?> AddAsync(SourceFormatNodeDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Calls the Encyclopedia Galactica SourceFormats endpoint and returns a
    ///     <see cref="List{T}" /> of <see cref="SourceFormatNodeDto" />s.
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{T}" /> representing asynchronous operation which includes the result.
    /// </returns>
    /// <exception cref="SdkException">In case of any error</exception>
    Task<List<SourceFormatNodeDto>?> GetAllAsync(CancellationToken cancellationToken = default);
}