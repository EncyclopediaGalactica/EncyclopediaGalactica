namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Structure;

using Dtos;

public interface IAddNewStructureCommand
{
    /// <summary>
    ///     Adds a new <see cref="Structure" /> to the system based on the provided information in the
    ///     <see cref="StructureDto" />.
    /// </summary>
    /// <param name="parentId">Id of the parent <see cref="Structure" /></param>
    /// <param name="structureDto">The <see cref="StructureDto" /> providing details</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    Task<StructureDto> AddNewAsync(
        long parentId,
        StructureDto structureDto,
        CancellationToken cancellationToken = default);
}