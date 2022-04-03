namespace EncyclopediaGalactica.SourceFormats.Mappers.Interfaces;

using Dtos;
using Entities;
using Exceptions.SourceFormatNode;

public interface ISourceFormatNodeMappers
{
    SourceFormatNode MapSourceFormatNodeModelToSourceFormatNode(IRequestModel model);

    /// <summary>
    ///     Maps a <see cref="SourceFormatNode" /> to a <see cref="SourceFormatNodeDto" />.
    ///     It does in the way the <see cref="SourceFormatNode" /> relations are not carried through, meaning
    ///     navigation properties are not populated.
    /// </summary>
    /// <param name="node">The <see cref="SourceFormatNodeDto" /> elem going to be mapped</param>
    /// <returns>
    ///     Returns a <see cref="IRequestModel" /> which property values equal to the original one.
    /// </returns>
    /// <exception cref="SourceFormatNodeMapperException">In case of any error.</exception>
    IRequestModel MapSourceFormatNodeToSourceFormatNodeModelInFlatFashion(SourceFormatNode node);

    /// <summary>
    ///     Maps <see cref="SourceFormatNode" /> elements of a list to <see cref="IRequestModel" />.
    ///     Does the mapping in the way the relations are not carried through,
    ///     meaning the navigation properties are not populated.
    /// </summary>
    /// <param name="sourceFormatNodes">The list of <see cref="IRequestModel" /></param>
    /// <returns>
    ///     <see cref="List{T}" /> representing the result of operation.
    /// </returns>
    /// <exception cref="SourceFormatNodeMapperException"> in case of any error.</exception>
    List<IRequestModel> MapSourceFormatNodesToSourceFormatNodeModelsInFlatFashion(
        List<SourceFormatNode> sourceFormatNodes);
}