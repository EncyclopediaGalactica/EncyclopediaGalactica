namespace EncyclopediaGalactica.SourceFormats.Mappers.Interfaces;

using Dtos;
using Entities;
using Exceptions.SourceFormatNode;

public interface ISourceFormatNodeMappers
{
    SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeDto dto);

    /// <summary>
    ///     Maps a <see cref="SourceFormatNode" /> to a <see cref="SourceFormatNodeDto" />.
    ///     It does in the way the <see cref="SourceFormatNode" /> relations are not carried through, meaning
    ///     navigation properties are not populated.
    /// </summary>
    /// <param name="node">The <see cref="SourceFormatNodeDto" /> elem going to be mapped</param>
    /// <returns>
    ///     Returns a <see cref="SourceFormatNodeDto" /> which property values equal to the original one.
    /// </returns>
    /// <exception cref="SourceFormatNodeMapperException">In case of any error.</exception>
    SourceFormatNodeDto MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(SourceFormatNode node);

    /// <summary>
    ///     Maps <see cref="SourceFormatNode" /> elements of a list to <see cref="SourceFormatNodeDto" />.
    ///     Does the mapping in the way the relations are not carried through,
    ///     meaning the navigation properties are not populated.
    /// </summary>
    /// <param name="sourceFormatNodes">The list of <see cref="SourceFormatNodeDto" /></param>
    /// <returns>
    ///     <see cref="List{T}" /> representing the result of operation.
    /// </returns>
    /// <exception cref="SourceFormatNodeMapperException"> in case of any error.</exception>
    List<SourceFormatNodeDto> MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(
        List<SourceFormatNode> sourceFormatNodes);
}